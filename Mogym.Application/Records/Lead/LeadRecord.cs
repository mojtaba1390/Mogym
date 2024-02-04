using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Lead
{
    public record LeadRecord
    {
        public string Name { get; init; }
        public string Mobile { get; init; }
        public string Message { get; init; }
    }
}
