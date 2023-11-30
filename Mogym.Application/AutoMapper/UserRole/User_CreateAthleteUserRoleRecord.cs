using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.UserRole;

namespace Mogym.Application.AutoMapper.UserRole
{
    public class User_CreateAthleteUserRoleRecord : global::AutoMapper.Profile
    {
        public User_CreateAthleteUserRoleRecord()
        {
            CreateMap<Domain.Entities.User, CreateAthleteUserRoleRecord>()
                .ForMember(x => x.UserId, z => z.MapFrom(a => a.Id));

        }
    }
}
