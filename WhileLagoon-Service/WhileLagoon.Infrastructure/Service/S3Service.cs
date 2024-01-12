using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Configuration;
using WhileLagoon.Application.Constant;
using WhileLagoon.Application.Contract.Service;
using WhileLagoon.Application.Dto.S3;
using WhileLagoon.Application.Model;

namespace WhileLagoon.Infrastructure.Service
{
    public class S3Service(IConfiguration configuration) : IS3Service
    {
        private readonly IConfiguration _configuration = configuration;
        public async Task<S3Response> UploadFileASync(CustomeS3Object s3Obj)
        {
            var s3Configuration = new S3Configuration();
            _configuration.GetSection(AppSetting.S3Configuration).Bind(s3Configuration);

            var credential = new BasicAWSCredentials(
                s3Configuration.S3AccessKey, 
                s3Configuration.S3SecretKey
            );

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.APSoutheast1
            };

            var response = new S3Response();
            try {
                // Create upload request
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = s3Obj.InputStream,
                    Key = s3Obj.Name,
                    BucketName = s3Configuration.S3ShopAvatarBucketName,
                    CannedACL = S3CannedACL.NoACL
                };
                // Create S3 Client
                using var client = new AmazonS3Client(credential, config);
                // Upload Utility to S3
                var transferUtility = new TransferUtility(client);
                // Upload to S3 
                await transferUtility.UploadAsync(uploadRequest);

                GetPreSignedUrlRequest preSignedUrlRequest = new()
                {
                    BucketName = s3Configuration.S3ShopAvatarBucketName,
                    Key = s3Obj.Name,
                    Expires = DateTime.UtcNow.AddHours(24)
                };

                string preSignedUrl = client.GetPreSignedURL(preSignedUrlRequest);
            
                response.StatusCode = 200;
                response.ObjectName = s3Obj.Name;
                response.PreSignedUrl = preSignedUrl;
            } 
            catch (AmazonS3Exception e) {
                response.StatusCode = (int)e.StatusCode;
                response.Message = e.Message;
            }
            catch (Exception e) {
                response.StatusCode = 500;
                response.Message = e.Message;
            }

            return response;
        }
    }
}