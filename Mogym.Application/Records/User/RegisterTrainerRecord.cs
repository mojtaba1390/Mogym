using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.User
{
    public record RegisterTrainerRecord
    {
        public string Mobile { get; init; }
        public string ConfirmCode { get; init; }
        public string NationalCode { get; init; }
        public string BirthDay { get; init; }
    }
}
