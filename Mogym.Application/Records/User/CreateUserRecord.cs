﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.User
{
    public record CreateUserRecord
    {
        public string Mobile { get; init; }
        public string ConfirmCode { get; init; }
    }
}