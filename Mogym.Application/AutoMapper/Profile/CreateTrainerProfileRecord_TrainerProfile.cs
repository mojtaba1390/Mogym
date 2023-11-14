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
    public class CreateTrainerProfileRecord_TrainerProfile:global::AutoMapper.Profile
    {
        public CreateTrainerProfileRecord_TrainerProfile()
        {
            CreateMap<CreateTrainerProfileRecord, TrainerProfile>()

                .ForMember(x => x.Biography, z => z.MapFrom(a => a.Biography))
                .ForMember(x => x.Id, z => z.MapFrom(a => a.Id))
                .ForMember(x => x.RowVersion, z => z.Ignore())
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });


        }

    }
}
