

using AutoMapper;
using ProductService.Application.Dto.Discount;
using ProductService.Domain.Entity;

namespace ProductService.Application.MappingProfile
{
    public class DiscountProfile: Profile
    {
        public DiscountProfile()
        {
            CreateMap<CreateDiscountReq, Discount>();
        }
    }
}