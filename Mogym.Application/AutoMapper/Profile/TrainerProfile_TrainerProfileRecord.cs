﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mogym.Application.Records.Profile;
using Mogym.Domain.Entities;

namespace Mogym.Application.AutoMapper.Profile
{
    public class TrainerProfile_TrainerProfileRecord:global::AutoMapper.Profile
    {
        public TrainerProfile_TrainerProfileRecord()
        {
            CreateMap<TrainerProfile, TrainerProfileRecord>()
                .ForMember(x => x.Id, z => z.MapFrom(a => a.User.Id))
                .ForMember(x => x.UserName, z => z.MapFrom(a => a.User.UserName))
                .ForMember(x => x.FirstName, z => z.MapFrom(a => a.User.FirstName))
                .ForMember(x => x.LastName, z => z.MapFrom(a => a.User.LastName))
                .ForMember(x => x.Mobile, z => z.MapFrom(a => a.User.Mobile))
                .ForMember(x => x.BirthDay, z => z.MapFrom(a => a.User.BirthDay))
                .ForMember(x => x.Email, z => z.MapFrom(a => a.User.Email))
                .ForMember(x => x.Biography, z => z.MapFrom(a => a.Biography))
                .ForMember(x => x.UserId, z => z.MapFrom(a => a.UserId));
        }
    }
}
