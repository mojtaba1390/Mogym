using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Interfaces.ILog;
using Mogym.Domain.Context;
using Mogym.Domain.Entities.Log;
using Mogym.Infrastructure;
using Mogym.Infrastructure.Interfaces.ILog;

namespace Mogym.Application.Services.Log
{
    public class UserLoggingService:IUserLoggingService
    {
        protected readonly IUserLoggingRepository _userLoggingRepository;

        public UserLoggingService(IUserLoggingRepository userLoggingRepository)
        {
            _userLoggingRepository=userLoggingRepository;
        }
        public async Task Save(string permalink, string ip)
        {
            var userLogging = new UserLogging()
            {
                Ip = ip,
                Permalink = permalink
            };
           await _userLoggingRepository.Save(userLogging);
        }
    }
}
