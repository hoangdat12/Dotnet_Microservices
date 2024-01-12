

using AutoMapper;
using WhileLagoon.Application.Dto.Cart;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.MappingProfile
{
    public class CartProfile: Profile
    {
        public CartProfile() 
        {
            CreateMap<AddProductToCart, CartProduct>();
        }
    }
}