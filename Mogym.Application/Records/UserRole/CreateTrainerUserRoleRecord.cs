﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.UserRole
{
    public record CreateTrainerUserRoleRecord
    {
        public int UserId { get; init; }
        public int RoleId { get; init; } = 3;//role id trainer
    }
}
