using MediatR;
using WhileLagoon.Application.Dto.Inventory;
using WhileLagoon.Application.Response;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.Feature.InventoryFeature.Command.ChangeQuantity
{
    public record UpdateInventoryCommand: IRequest<BaseResponse>
    {
        public User User {get; set;}
        public UpdateInventoryReq Req {get; set;}
    }
}