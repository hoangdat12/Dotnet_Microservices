using AutoMapper;
using System.Security.Claims;
using UserGRPCService;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.MappingProfile
{
    public class UserProfile: Profile
    {
        public UserProfile() {
            CreateMap<ClaimsPrincipal, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(
                    src => new Guid(src.FindFirst("userId").Value.ToString()))
                )
                .ForMember(dest => dest.Email, opt => opt.MapFrom(
                    src => src.FindFirst("email").Value.ToString())
                )
                .ForMember(dest => dest.Role, opt => opt.MapFrom(
                    src => src.FindFirst("role").Value.ToString())
                )
                .ReverseMap();

            CreateMap<User, GetUserRes>();
        }
    }
}
