using MediatR;
using Microsoft.Extensions.Configuration;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Dto.S3;
using WhileLagoon.Application.Model;

namespace WhileLagoon.Application.Feature.ShopFeature.Command.UploadShopAvatar
{
    public class UploadShopAvatarCommandHandler(
        IConfiguration configuration,
        IS3Service s3Service
    ) : IRequestHandler<UploadShopAvatarCommand, S3Response>
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IS3Service _s3Service = s3Service;
        public async Task<S3Response> Handle(UploadShopAvatarCommand request, CancellationToken cancellationToken)
        {
            S3Configuration s3Configuration = new();
            _configuration.GetSection(AppSetting.S3Configuration).Bind(s3Configuration);

            var file = request.File;

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);

            var fileExtention = Path.GetExtension(file.Name);

            var objName = $"{Guid.NewGuid()}.{fileExtention}";
            CustomeS3Object s3Object = new() 
            {
                Name = objName,
                InputStream = memoryStream,
                BucketName = s3Configuration.S3ShopAvatarBucketName
            };

            return await _s3Service.UploadFileASync(s3Object);
        }
    }
}