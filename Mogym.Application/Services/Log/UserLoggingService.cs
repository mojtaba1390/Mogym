using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Interfaces.ILog;
using Mogym.Domain.Context;
using Mogym.Domain.Entities.Log;
using Mogym.Infrastructure;

namespace Mogym.Application.Services.Log
{
    public class UserLoggingService:IUserLoggingService
    {
        protected readonly MogymLogContext _context;

        public UserLoggingService(MogymLogContext context)
        {
            _context = context;
        }
        public async Task Save(string permalink, string ip)
        {
            var userLogging = new UserLogging()
            {
                Ip = ip,
                Permalink = permalink
            };
           await _context.UserLogging.AddAsync(userLogging);
        }
    }
}
