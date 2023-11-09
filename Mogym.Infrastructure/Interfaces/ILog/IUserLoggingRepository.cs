using Mogym.Domain.Entities.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Infrastructure.Interfaces.ILog
{
    public  interface IUserLoggingRepository
    {
        Task Save(UserLogging userLogging);

    }
}
