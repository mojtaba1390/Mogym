using Mogym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Profile;

namespace Mogym.Application.AutoMapper.Profile
{
    public class TrainerProfile_TrainerProfileRecord:global::AutoMapper.Profile
    {
        public TrainerProfile_TrainerProfileRecord()
        {
            CreateMap<TrainerProfile, TrainerProfileRecord>()
                .ForMember(x => x.TrainerId, z => z.MapFrom(a => a.Id))
                .ForMember(x => x.Biography, z => z.MapFrom(a => a.Biography))
                .ForMember(x => x.FirstName, z => z.MapFrom(a => a.User.FirstName))
                .ForMember(x => x.LastName, z => z.MapFrom(a => a.User.LastName))
                .ForMember(x => x.UserName, z => z.MapFrom(a => a.User.UserName))
                .ForMember(x => x.ProfilePic, z => z.MapFrom(a => a.User.ProfilePic))
                .ForMember(x => x.TrainerPlanCostRecords, z => z.MapFrom(a => a.TrainerPlanCosts))
                .ForMember(x => x.TrainerAchievementRecords, z => z.MapFrom(a => a.TrainerAchievements));
        }
    }
}
