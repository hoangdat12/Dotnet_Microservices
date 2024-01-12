using WhileLagoon.Application.Dto.S3;
using WhileLagoon.Application.Model;

namespace WhileLagoon.Application.Contract.Service
{
    public interface IS3Service
    {
        Task<S3Response> UploadFileASync(CustomeS3Object s3Obj);
    }
}