using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.Interfaces
{
    public interface ISmsService
    {
        Task<ResultDTO> SendSms(string reciverMobile, string message);
        Task<ResultDTO> SendOTP(string mobile, string token, string template);
        Task<ResultDTO> SendOTP3Token(string mobile, string token, string token2, string token3, string template);
        Task<ResultDTO> SendOTP2Token(string mobile, string token, string token2,  string template);
    }
}
