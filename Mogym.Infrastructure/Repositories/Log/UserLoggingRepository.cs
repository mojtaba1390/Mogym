using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Domain.Entities.Log;
using Mogym.Infrastructure.Interfaces.ILog;

namespace Mogym.Infrastructure.Repositories.Log
{
    public class UserLoggingRepository:IUserLoggingRepository
    {
        public async Task Save(UserLogging userLogging)
        {
            var t = 0;
        }
    }
}
