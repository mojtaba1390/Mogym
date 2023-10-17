using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Menu
{
    public record ControllerAndActionRecord
    {
        public string Controller { get; init; }
        public string ControllerDisplayName { get; init; }
    }
}
