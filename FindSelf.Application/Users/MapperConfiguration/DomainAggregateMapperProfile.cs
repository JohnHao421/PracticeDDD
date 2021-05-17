using AutoMapper;
using FindSelf.Application.Users;
using FindSelf.Domain.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Application.Users.MapperConfiguration
{
    public class DomainAggregateMapperProfile : Profile
    {
        public DomainAggregateMapperProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
