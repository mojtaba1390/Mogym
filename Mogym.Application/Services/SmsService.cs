using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Interfaces;
using Mogym.Common;

namespace Mogym.Application.Services
{
    public class SmsService:ISmsService
    {
        private readonly IBaseService _baseService;
        private static string apiKey= "6448696B4A593266524D4D5750577A6964497579744E7171667079437955474E6A464B622F446665305A733D";
        private static string sendrNumber = "2000500666";
        public SmsService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResultDTO> SendSms( string reciverMobile, string message)
        {
            try
            {
                var request =
                    $"https://api.kavenegar.com/v1/{apiKey}/sms/send.json?receptor={reciverMobile}&sender=&message={message}";

                var res = await _baseService.SendAsync(new RequestDTO() {ApiType = EnumApiType.Get, Url = request});
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<ResultDTO> SendOTP(string mobile, string token, string tmplate)
        {
            throw new NotImplementedException();
        }
    }
}
