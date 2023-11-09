using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Interfaces.ILog
{
    public interface IUserLoggingService
    {
        Task Save(string permalink, string ip);
    }
}
