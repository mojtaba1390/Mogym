using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.UserRole;

namespace Mogym.Application.AutoMapper.UserRole
{
    public class CreateTrainerUserRoleRecord_UserRole:global::AutoMapper.Profile
    {
        public CreateTrainerUserRoleRecord_UserRole()
        {
            CreateMap<CreateTrainerUserRoleRecord, Domain.Entities.UserRole>()
                .ForMember(x => x.UserId, z => z.MapFrom(a => a.UserId))
                .ForMember(x => x.RoleId, z => z.MapFrom(a => a.RoleId));

        }
    }
}
