using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Profile
{
    public record TrainersRecord
    {
        public int Id { get; init; }
        public string  FullName { get; init; }
        public string  ProfilePic { get; init; }
        public string UserName { get; init; }

    }
}
