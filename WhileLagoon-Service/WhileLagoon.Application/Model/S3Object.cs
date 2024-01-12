namespace WhileLagoon.Application.Model
{
    public class CustomeS3Object
    {
        public string Name {get; set;} = null;
        public MemoryStream InputStream {get; set;} = null!;
        public string BucketName { get; set; } = null!;
    }
}