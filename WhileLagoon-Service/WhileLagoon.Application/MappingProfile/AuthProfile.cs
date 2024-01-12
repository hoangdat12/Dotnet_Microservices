using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhileLagoon.Application.Feature.AuthFeature.Command.Register;
using WhileLagoon.Domain.Entity;

namespace WhileLagoon.Application.MappingProfile
{
    public class AuthProfile: Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterCommand, User>();
        }
    }
}
