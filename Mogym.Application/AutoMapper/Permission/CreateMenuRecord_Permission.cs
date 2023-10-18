using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Menu;
using Mogym.Common;

namespace Mogym.Application.AutoMapper.Permission
{
    public class CreateMenuRecord_Permission:Profile
    {
        public CreateMenuRecord_Permission()
        {
            CreateMap<CreateMenuRecord, Domain.Entities.Permission>()
                .ForMember(x => x.EnglishName, frm => frm.MapFrom(z => z.EnglishName))
                .ForMember(x => x.PersianName, frm => frm.MapFrom(z => z.PersianName))
                .ForMember(x => x.IsActive, frm => frm.MapFrom(z => z.IsActive))
                .ForMember(x => x.ParentId,
                    frm => frm.MapFrom(z => (z.HasParentInPermission == EnumYesNo.Yes ? z.PermissionParentId : null)));

        }
    }
}
