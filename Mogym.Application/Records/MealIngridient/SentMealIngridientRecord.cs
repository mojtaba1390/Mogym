using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.MealIngridient
{
    public record SentMealIngridientRecord
    {
        public int Id { get; init; }
        public int IngridientId { get; init; }
        public string IngridientTitle { get; init; }
        public double? Count { get; init; }
        public string?  Size { get; init; }
        public double? Amount { get; init; }
    }
}
