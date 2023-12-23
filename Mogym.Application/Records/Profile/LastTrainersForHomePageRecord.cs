using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Profile
{
    public record LastTrainersForHomePageRecord
    {
        public string FullName { get; init; }
        public string UserName { get; init; }
        public string ProfilePic { get; init; }
        public string Biography { get; init; }
    }
}
