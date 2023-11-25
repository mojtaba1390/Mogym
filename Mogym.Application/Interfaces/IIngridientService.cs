using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Ingridient;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface IIngridientService
    {
        Task<List<IngredientRecord>> GetAll();
        Task AddAsync(IngredientRecord ingredientRecord);
    }
}
