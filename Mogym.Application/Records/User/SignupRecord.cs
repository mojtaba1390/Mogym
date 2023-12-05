using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.User
{
    public record SignupRecord
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }

        public string Email { get; init; }
        public string Password { get; init; }
    }
}
