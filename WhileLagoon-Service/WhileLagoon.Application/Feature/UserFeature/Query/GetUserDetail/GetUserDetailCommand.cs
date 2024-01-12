using MediatR;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.UserFeature.Query.GetUserDetail
{
    public record GetUserDetailCommand(Guid UserId): IRequest<User>
    {
        
    }
}