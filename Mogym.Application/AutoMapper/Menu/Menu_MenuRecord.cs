using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Menu;

namespace Mogym.Application.AutoMapper.Menu
{
    public class Menu_MenuRecord:global::AutoMapper.Profile
    {
        public Menu_MenuRecord()
        {
            CreateMap<Domain.Entities.Menu, MenuRecord>()
                .ForMember(x => x.ParentMenu, frm => frm.MapFrom(z => z.Menu_Menu));
        }
    }
}
