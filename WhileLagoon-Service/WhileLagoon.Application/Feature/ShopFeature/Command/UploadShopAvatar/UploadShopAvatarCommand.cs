using MediatR;
using Microsoft.AspNetCore.Http;
using WhileLagoon.Application.Dto.S3;

namespace WhileLagoon.Application.Feature.ShopFeature.Command.UploadShopAvatar
{
    public record UploadShopAvatarCommand: IRequest<S3Response>
    {
        public IFormFile File {get; set;}
    }
}