using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Profile
{
    public record TrainerProfileRecord
    {
        public int Id { get; init; }
        public string? UserName { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string Mobile { get; init; }
        public string? BirthDay { get; init; }
        public string? Email { get; init; }

        public string? Biography { get; set; }

    }
}
