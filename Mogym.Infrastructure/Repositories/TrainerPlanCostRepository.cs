using Mogym.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mogym.Infrastructure.Interfaces;

namespace Mogym.Infrastructure.Repositories
{
    public class TrainerPlanCostRepository:Repository<TrainerPlanCost>, ITrainerPlanCostRepository
    {
        public TrainerPlanCostRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
