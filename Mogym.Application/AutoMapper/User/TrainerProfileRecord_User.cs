using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Profile;

namespace Mogym.Application.AutoMapper.User
{
    public class TrainerProfileRecord_User:global::AutoMapper.Profile
    {
        public TrainerProfileRecord_User()
        {
            CreateMap<TrainerProfileRecord, Domain.Entities.User>()
                .ForMember(x => x.BirthDay, z => z.MapFrom(a => a.BirthDay))
                .ForMember(x => x.FirstName, z => z.MapFrom(a => a.FirstName))
                .ForMember(x => x.LastName, z => z.MapFrom(a => a.LastName))
                .ForMember(x => x.UserName, z => z.MapFrom(a => a.UserName))
                .ForMember(x => x.Id, z => z.MapFrom(a => a.UserId))
                .ForMember(x => x.ProfilePic, z => z.MapFrom(a => a.ProfilePic.FileName))
                .ForMember(x => x.RowVersion, z => z.Ignore());
        }
    }
}
