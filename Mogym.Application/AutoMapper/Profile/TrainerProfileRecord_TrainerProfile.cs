using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Profile;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper.Profile
{
    public  class TrainerProfileRecord_TrainerProfile:global::AutoMapper.Profile
    {
        public TrainerProfileRecord_TrainerProfile()
        {
            CreateMap<TrainerProfileRecord, TrainerProfile>()
                .ForPath(x => x.User.FirstName, z => z.MapFrom(a => a.FirstName))
                .ForPath(x => x.User.LastName, z => z.MapFrom(a => a.LastName))
                .ForPath(x => x.User.UserName, z => z.MapFrom(a => a.UserName))
                .ForPath(x => x.User.BirthDay, z => z.MapFrom(a => a.BirthDay))
                .ForPath(x => x.User.Id, z => z.MapFrom(a => a.UserId))
                .ForMember(x => x.Biography, z => z.MapFrom(a => a.Biography))
                .ForMember(x => x.Id, z => z.MapFrom(a => a.Id))
                .ForMember(x => x.ProfilePic, z => z.MapFrom(a => a.ProfilePic != null ? a.ProfilePic.FileName : null))
                .ForMember(x => x.RowVersion, z => z.Ignore())
                .ForPath(x => x.User.RowVersion, z => z.Ignore());


        }

    }
}
