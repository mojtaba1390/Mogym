using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mogym.Domain.Entities.Log;
using Mogym.Infrastructure.Interfaces.ILog;

namespace Mogym.Infrastructure.Repositories.Log
{
    public class SmsLogRepository:Repository<SmsLog>,ISmsLogRepository
    {
        public SmsLogRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
