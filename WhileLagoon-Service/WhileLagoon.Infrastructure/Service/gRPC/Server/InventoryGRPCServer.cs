
using AutoMapper;
using Grpc.Core;
using InventoryGRPCService;
using WhileLagoon.Application.Contract.Repository;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Infrastructure.Service
{
    public class InventoryGRPCServer(
        IInventoryRepository inventoryRepository,
        IMapper mapper
    ): InventoryGRPC.InventoryGRPCBase
    {
        private readonly IInventoryRepository _inventoryRepository = inventoryRepository;
        private readonly IMapper _mapper = mapper;

        public override async Task<GetInventoryRes> GetInventory(GetInventoryReq request, ServerCallContext context)
        {
            Inventory foundInventory = await _inventoryRepository.GetByProductIdAsync(
                new Guid(request.ProductId)
            );

            return _mapper.Map<GetInventoryRes>(foundInventory);
        }
    }
}