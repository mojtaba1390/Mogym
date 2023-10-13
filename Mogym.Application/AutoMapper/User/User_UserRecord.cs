﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Permission;
using Mogym.Application.Records.Role;
using Mogym.Application.Records.User;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper.User
{
    public class User_UserRecord:Profile
    {
        public User_UserRecord()
        {

            CreateMap<Domain.Entities.User, UserRecord>()
                .ForMember(dest => dest.Roles,
                    opt =>
                        opt.MapFrom(src => src.UserRoles.Select(ur => ur.UserRole_Role)));






        }
    }
}
