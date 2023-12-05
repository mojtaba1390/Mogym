using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Mogym.Application.Interfaces.ILog;
using Mogym.Domain.Entities.Log;
using Newtonsoft.Json;

namespace Mogym.Application.Services.Log
{
    public class SmsLogService:ISmsLogService
    {
        private readonly  HttpClient _client;

        public SmsLogService(HttpClient client)
        {
            _client = client;
        }
        public async Task SendConfirmSmsCode(string mobile,string smsCode)
        {
            string path =
                "https://api.kavenegar.com/v1/6448696B4A593266524D4D5750577A6964497579744E7171667079437955474E6A464B622F446665305A733D/verify/lookup.json?receptor=" +
                mobile + "&token=" + smsCode + "&template=MogymConfirmSmsCode";
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string stringResult = await response.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<SmsLog>(stringResult);

            }
            else
            {

            }

        }



    }
}
