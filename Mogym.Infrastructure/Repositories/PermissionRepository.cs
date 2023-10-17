using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mogym.Domain.Entities;
using Mogym.Infrastructure.Interfaces;

namespace Mogym.Infrastructure.Repositories
{
    public class PermissionRepository:Repository<Permission>,IPermissionRepository
    {
        public PermissionRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
