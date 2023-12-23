using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Profile;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper.Profile
{
    public class TrainerProfile_LastTrainersForHomePageRecord:global::AutoMapper.Profile
    {
        public TrainerProfile_LastTrainersForHomePageRecord()
        {
            CreateMap<TrainerProfile, LastTrainersForHomePageRecord>()
                .ForMember(x => x.FullName,
                    z => z.MapFrom(a => a.User.FirstName + " " + a.User.LastName))
                .ForMember(x => x.ProfilePic,
                    z => z.MapFrom(a => a.User.ProfilePic))
                .ForMember(x => x.UserName,
                    z => z.MapFrom(a => a.User.UserName))
                .ForMember(x => x.Biography,
                    z => z.MapFrom(a => a.Biography));
        }
    }
}
