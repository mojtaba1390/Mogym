using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Profile;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper.Profile
{
    public class Profile_TrainerPaymentRecord:global::AutoMapper.Profile
    {
        public Profile_TrainerPaymentRecord()
        {
            CreateMap<TrainerProfile, TrainerPaymentRecord>()
                .ForMember(x => x.CartNumber,
                    z => z.MapFrom(a => a.CartNumber))
                .ForMember(x => x.CartOwner,
                    z => z.MapFrom(a => a.CartOwnerName));
        }
    }
}
