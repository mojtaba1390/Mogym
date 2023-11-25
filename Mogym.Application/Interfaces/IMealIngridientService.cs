using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.MealIngridient;

namespace Mogym.Application.Interfaces
{
    public interface IMealIngridientService
    {
        Task<List<MealIngridientRecord>> GetMealIngridients(int mealId);
        void AddOrUpdateSets(List<MealIngridientRecord> mealIngridientRecords);
    }
}
