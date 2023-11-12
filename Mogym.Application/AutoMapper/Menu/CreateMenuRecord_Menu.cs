using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Menu;

namespace Mogym.Application.AutoMapper.Menu
{
    public class CreateMenuRecord_Menu:global::AutoMapper.Profile
    {
        public CreateMenuRecord_Menu()
        {
            CreateMap<CreateMenuRecord, Domain.Entities.Menu>()
                .ForMember(x => x.EnglishName, frm => frm.MapFrom(z => z.EnglishName))
                .ForMember(x => x.PersianName, frm => frm.MapFrom(z => z.PersianName))
                .ForMember(x => x.IsActive, frm => frm.MapFrom(z => z.IsActive))
                .ForMember(x => x.ParentId, frm => frm.MapFrom(z => z.ParentId))
                .ForMember(x => x.Link, frm => frm.MapFrom(z => z.Link));
        }
    }
}
