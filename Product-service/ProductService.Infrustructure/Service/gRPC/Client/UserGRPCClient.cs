using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using ProductService.Application.Contract.Infrastructure;
using ProductService.Infrustructure.Common;
using UserGRPCService;

namespace ProductService.Infrustructure.Service
{
    public class UserGRPCClient(
        IConfiguration configuration
    ): BaseClient(configuration), IUserGRPCClient
    {
        public async Task<GetUserRes> GetUserAsync(string UserId)
        {
            using var channel = GrpcChannel.ForAddress(_grpcServer.BaseServer);
            var client = new UserGRPC.UserGRPCClient(channel);

            GetUserReq request = new()
            {
                UserId = UserId
            };

            return await client.GetUserAsync(request);
        }

        public async Task<VerifyAccessTokenRes> VerifyAccessTokenAsync(string UserId, string Token)
        {
            using var channel = GrpcChannel.ForAddress(_grpcServer.BaseServer);
            var client = new UserGRPC.UserGRPCClient(channel);

            VerifyAccessTokenReq request = new()
            {
                UserId = UserId,
                Token = Token
            };

            return await client.VerifyAccessTokenAsync(request);
        }
    }
}