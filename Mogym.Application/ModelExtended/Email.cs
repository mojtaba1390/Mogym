using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Common.ModelExtended
{
    public class Email
    {
        public string From { get; set; } = "ramezannia.mojtaba@gmail.com";
        public string SmtpServer { get; set; } = "smtp.gmail.com";
        public int Port { get; set; } = 465;
        public string UserName { get; set; } = "ramezannia.mojtaba@gmail.com";
        public string Password { get; set; } = "nhki tkgi yxzw xmfi";
    }
}
