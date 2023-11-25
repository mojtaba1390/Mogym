using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.Records.MealIngridient
{
    public record MealIngridientRecord
    (
        int? Id,
        double? Count,
        int? Size,
        double? Amount,
        int? MealId,
        int? IngridientId
    );

}
