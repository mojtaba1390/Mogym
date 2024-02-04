using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Lead;

namespace Mogym.Application.AutoMapper.Lead
{
    public class LeadRecord_Lead:global::AutoMapper.Profile
    {
        public LeadRecord_Lead()
        {
            CreateMap<LeadRecord, Domain.Entities.Lead>().ReverseMap();
        }
    }
}
