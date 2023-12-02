using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Domain.Context;
using Mogym.Domain.Entities.Log;
using Mogym.Infrastructure.Interfaces.ILog;

namespace Mogym.Infrastructure.Repositories.Log
{
    public class UserLoggingRepository:IUserLoggingRepository
    {
        private readonly MogymLogContext _context;
        public UserLoggingRepository(MogymLogContext context)
        {
            _context=context;
        }
        public async Task Save(UserLogging userLogging)
        {
            await _context.UserLogging.AddAsync(userLogging);
            await _context.SaveChangesAsync();
        }
    }
}
