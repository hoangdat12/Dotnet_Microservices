using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhileLagoon.Application.Dto.S3
{
    public class S3Response
    {
        public int StatusCode {get; set;} = 200;
        public string ObjectName {get; set;}
        public string Message {get; set;} = "Successfully!";
        public string PreSignedUrl {get; set;}
    }
}