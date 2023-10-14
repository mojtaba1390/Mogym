using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Interfaces.ILog;
using Mogym.Infrastructure;
using Mogym.Infrastructure.Interfaces.Log;

namespace Mogym.Application.Services.Log
{
    public class SerilogService:ISeriLogService
    {
        private IUnitOfWork _unitOfWork;
        public SerilogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void LogInformation(string message)
        {
            _unitOfWork.SeriLogRepository.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _unitOfWork.SeriLogRepository.LogWarning(message);
        }

        public void LogError(string message, Exception exception)
        {
            _unitOfWork.SeriLogRepository.LogError(message,exception);
        }
    }
}
