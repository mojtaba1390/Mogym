using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.AutoMapper.SupplimentPlan;
using Mogym.Application.Records.Meal;
using Mogym.Application.Records.Workout;
using Mogym.Domain.Entities;

namespace Mogym.Application.Records.Plan
{
    public record PlanDetailsRecord
    {
        public int PlanId { get; init; }
        public string? Description { get; init; }

        public List<WorkoutRecord> WorkoutRecords { get; init; }
        public List<MealRecord> MealRecords { get; init; }
        public List<SupplimentPlanRecord> SupplimentPlans { get; init; }
    }
}
