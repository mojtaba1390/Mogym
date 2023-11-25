using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Suppliment;

namespace Mogym.Application.AutoMapper.Suppliment
{
    public class Suppliment_SupplimentRecord:global::AutoMapper.Profile
    {
        public Suppliment_SupplimentRecord()
        {
            CreateMap<Domain.Entities.Suppliment, SupplimentRecord>().ReverseMap();
        }
    }
}
