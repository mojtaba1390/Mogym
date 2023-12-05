using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Domain.Entities.Log;

namespace Mogym.Infrastructure.Interfaces.ILog
{
    public interface ISmsLogRepository
    {
         Task Save(SmsLog smsLog);
    }
}
