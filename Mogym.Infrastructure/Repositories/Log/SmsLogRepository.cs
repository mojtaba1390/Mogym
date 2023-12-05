using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mogym.Domain.Context;
using Mogym.Domain.Entities.Log;
using Mogym.Infrastructure.Interfaces.ILog;

namespace Mogym.Infrastructure.Repositories.Log
{
    public class SmsLogRepository:ISmsLogRepository
    {
        private readonly MogymLogContext _context;
        public SmsLogRepository(MogymLogContext context)
        {
            _context = context;
        }
        public async Task Save(SmsLog smsLog)
        {
            await _context.SmsLog.AddAsync(smsLog);
            await _context.SaveChangesAsync();
        }
    }
}
