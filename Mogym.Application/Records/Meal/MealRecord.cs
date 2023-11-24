using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Meal
{
    public record MealRecord
    (
        int? PlanId,
        int? Id,
        string Title
    );
}
