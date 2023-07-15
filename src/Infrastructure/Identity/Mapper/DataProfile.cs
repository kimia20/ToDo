using AutoMapper;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.UserAggregate.Dto;

namespace Infrastructure.Identity.Mapper
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<UserDto, ApplicationUser>().ConstructUsing(u => new ApplicationUser
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber
            }).ReverseMap();
        }
    }
}

