
using Microsoft.Extensions.Configuration;
using OrderService.Application.Constant;
using OrderService.Application.Model;

namespace OrderService.Infrastructure.Common
{
    public class BaseClient
    {
        protected readonly IConfiguration _configuration;
        protected readonly GrpcServer _grpcServer;

        public BaseClient(IConfiguration configuration) {
            _configuration = configuration;

            GrpcServer grpcServer = new();
            _configuration.GetSection(AppSetting.GRPCServer).Bind(grpcServer);

            _grpcServer = grpcServer;
        }
    }
}