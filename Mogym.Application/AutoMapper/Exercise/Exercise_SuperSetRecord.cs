﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Exercise;

namespace Mogym.Application.AutoMapper.Exercise
{
    public class Exercise_SuperSetRecord:global::AutoMapper.Profile
    {
        public Exercise_SuperSetRecord()
        {
            CreateMap<Domain.Entities.Exercise, SuperSetRecord>();
        }
    }
}
