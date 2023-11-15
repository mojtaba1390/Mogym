using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.User
{
    public record LoginRecord
    {
        public string Mobile { get; init; }
        //public string Password { get; init; }
        public string? CofirmCode { get; init; }
        public string? ReturnUrl { get; init; }
    }
}
