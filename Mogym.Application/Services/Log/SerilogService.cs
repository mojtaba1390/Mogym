using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure.Interfaces.Log;

namespace Mogym.Application.Services.Log
{
    public class SeriLogService:ISeriLogService
    {
        private readonly ISeriLogRepository _seriLogRepository;
        public SeriLogService(ISeriLogRepository seriLogRepository)
        {
            _seriLogRepository = seriLogRepository;
        }
        public void LogInformation(string message)
        {
            _seriLogRepository.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _seriLogRepository.LogWarning(message);
        }

        public void LogError(string message, Exception exception)
        {
            _seriLogRepository.LogError(message, exception);
        }
    }
}
