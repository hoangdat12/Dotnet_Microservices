
using System.Security.Claims;
using AutoMapper;
using ProductService.Application.Dto;

namespace ProductService.Application.MappingProfile
{
    public class UserProfile: Profile
    {
        public UserProfile() {
            CreateMap<ClaimsPrincipal, UserDecode>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(
                    src => new Guid(src.FindFirst("userId").Value.ToString()))
                )
                .ForMember(dest => dest.Email, opt => opt.MapFrom(
                    src => src.FindFirst("email").Value.ToString())
                )
                .ForMember(dest => dest.Role, opt => opt.MapFrom(
                    src => src.FindFirst("role").Value.ToString())
                )
                .ReverseMap();
        }
    }
}