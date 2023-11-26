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
    public class SupplimentPlanDetailRepository:Repository<SupplimentPlanDetail>, ISupplimentPlanDetailRepository
    {
        public SupplimentPlanDetailRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
