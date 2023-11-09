using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.UserRole;

namespace Mogym.Application.AutoMapper.UserRole
{
    public class User_UserRoleRecord:Profile
    {
        public User_UserRoleRecord()
        {
            CreateMap<Domain.Entities.User, CreateTrainerUserRoleRecord>()
                .ForMember(x => x.UserId, z => z.MapFrom(a => a.Id));

        }
    }
}
