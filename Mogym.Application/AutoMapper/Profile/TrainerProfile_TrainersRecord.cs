using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Profile;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper.Profile
{
    public class TrainerProfile_TrainersRecord : global::AutoMapper.Profile
    {
        public TrainerProfile_TrainersRecord()
        {
            CreateMap<TrainerProfile, TrainersRecord>()
                .ForMember(x => x.Id,
                    z => z.MapFrom(a => a.Id))
                .ForMember(x => x.ProfilePic,
                    z => z.MapFrom(a => a.User.ProfilePic))
                .ForMember(x => x.UserName,
                    z => z.MapFrom(a => a.User.UserName))
                .ForMember(x => x.FullName,
                    z => z.MapFrom(a => a.User.FirstName + " " + a.User.LastName));
        }
    }
}
