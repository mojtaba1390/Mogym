using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.User
{
    public record ConfirmRegisterRecord
    {
        public string Mobile { get; init; }
        public int ConfirmCode { get; init; }
    }
}
