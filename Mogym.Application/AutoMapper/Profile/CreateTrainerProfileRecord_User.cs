using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Profile;

namespace Mogym.Application.AutoMapper.Profile
{
    public class CreateTrainerProfileRecord_User:global::AutoMapper.Profile
    {
        public CreateTrainerProfileRecord_User()
        {
            CreateMap<CreateTrainerProfileRecord, Domain.Entities.User>()
                .ForMember(x => x.RowVersion, z => z.Ignore())
                .ForMember(x => x.Email, z => z.Ignore())
                .ForMember(x => x.Password, z => z.Ignore())
                .ForMember(x => x.Gender, z => z.Ignore())
                .ForMember(x => x.Status, z => z.Ignore())
                .ForMember(x => x.UniqeUserName, z => z.Ignore());
        }
    }
}
