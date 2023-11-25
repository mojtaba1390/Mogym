using Mogym.Domain.Entities;
using Mogym.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mogym.Infrastructure.Repositories
{
    public class IngredientRepository:Repository<Ingredient>, IIngridientRepository
    {
        public IngredientRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
