using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Contract.Infrastructure;
using OrderService.Infrastructure.Common;
using UserGRPCService;

namespace OrderService.Infrustructure.Service
{
    public class UserGRPCClient: BaseClient, IUserGRPCClient
    {
        private readonly UserGRPC.UserGRPCClient _client;
        public UserGRPCClient(IConfiguration configuration): base(configuration)
        {
            var channel = GrpcChannel.ForAddress(_grpcServer.ProductServer);
            _client = new UserGRPC.UserGRPCClient(channel);
        }

        public async Task<GetUserRes> GetUserAsync(string UserId)
        {
            GetUserReq request = new()
            {
                UserId = UserId
            };

            return await _client.GetUserAsync(request);
        }

        public async Task<VerifyAccessTokenRes> VerifyAccessTokenAsync(string UserId, string Token)
        {
            VerifyAccessTokenReq request = new()
            {
                UserId = UserId,
                Token = Token
            };

            return await _client.VerifyAccessTokenAsync(request);
        }
    }
}