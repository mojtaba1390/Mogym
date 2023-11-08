using Mogym.Application.Records.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Mogym.Application.AutoMapper.UserRole
{
    public class CreateAthleteUserRoleRecord_UserRole:Profile
    {
        public CreateAthleteUserRoleRecord_UserRole()
        {
            CreateMap<CreateAthleteUserRoleRecord, Domain.Entities.UserRole>()
                .ForMember(x => x.UserId, z => z.MapFrom(a => a.UserId))
                .ForMember(x => x.RoleId, z => z.MapFrom(a => a.RoleId));
        }
    }
}
