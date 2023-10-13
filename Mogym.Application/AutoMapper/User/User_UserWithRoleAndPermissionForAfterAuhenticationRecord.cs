using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.User;

namespace Mogym.Application.AutoMapper.User
{
    public class User_UserWithRoleAndPermissionForAfterAuhenticationRecord:Profile
    {
        public User_UserWithRoleAndPermissionForAfterAuhenticationRecord()
        {
            CreateMap<Domain.Entities.User, UserWithRoleAndPermissionForAfterAuhenticationRecord>()
                .ForMember(x => x.Roles, frm => frm.MapFrom(z => new RoleForAuthenticationRecord()
                {
                    Id = z.UserRoles.Select(x => x.UserRole_Role.Id).First(),
                    EnglishName = z.UserRoles.Select(x => x.UserRole_Role.EnglishName).First(),
                    PersianName = z.UserRoles.Select(x => x.UserRole_Role.PersianName).First()
                }));
            //.ForMember(x => x.Roles.SelectMany(z => z.Permissions), frm => frm.MapFrom(a =>
            //    new PermissionForAuthenticationRecord()
            //    {
            //        Id = a.UserRoles.Select(q => q.UserRole_Role.Permissions.Select(w => w.Id).FirstOrDefault())
            //            .FirstOrDefault(),
            //        EnglishName = a.UserRoles
            //            .Select(q => q.UserRole_Role.Permissions.Select(w => w.EnglishName).FirstOrDefault())
            //            .FirstOrDefault(),
            //        PersianName = a.UserRoles
            //            .Select(q => q.UserRole_Role.Permissions.Select(w => w.PersianName).FirstOrDefault())
            //            .FirstOrDefault(),

            //    }));
        }
    }
}
