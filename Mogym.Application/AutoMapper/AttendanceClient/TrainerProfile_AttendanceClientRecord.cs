using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.User;

namespace Mogym.Application.AutoMapper.AttendanceClient
{
    public class TrainerProfile_AttendanceClientRecord:global::AutoMapper.Profile
    {
        public TrainerProfile_AttendanceClientRecord()
        {
            CreateMap<Domain.Entities.TrainerProfile, AttendanceClientRecord>()
                .ForMember(x => x.TrainerPlanCosts,
                    z => z.MapFrom(a => a.TrainerPlanCosts));
        }
    }
}
