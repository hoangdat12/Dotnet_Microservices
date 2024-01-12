
using AutoMapper;
using InventoryGRPCService;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.MappingProfile
{
    public class InventoryProfile: Profile
    {
        public InventoryProfile() {
            CreateMap<Inventory, GetInventoryRes>();
        } 
    }
}