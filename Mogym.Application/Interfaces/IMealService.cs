using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Meal;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface IMealService
    {
        Task AddOrUpdate(List<MealRecord> mealRecords);
        Task Edit(int id, string title);
        Task Delete(int deleteId);
        Task<List<SentMealRecord>> GetSentDietDetail(int planId);
        Task<Meal> GetByIdAsync(int mealId);
    }
}
