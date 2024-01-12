﻿
namespace WhileLagoon.Application.Dto
{
    public class JwtOption
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireIn { get; set; }
    }
}
