using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Suppliment
{
    public record SentSupplimentRecord
    {
        public int Id { get; init; }
        public int SupplimentId { get; init; }
        public string SupplimentTitle { get; init; }
        public string? Scale { get; init; }
        public double? Amount { get; init; }

    }
}
