using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mogym.Domain.Entities;

namespace Mogym.Domain.Configuration
{
    public class SupplimentPlanDetailConfiguration:IEntityTypeConfiguration<SupplimentPlanDetail>
    {
        public void Configure(EntityTypeBuilder<SupplimentPlanDetail> builder)
        {
            throw new NotImplementedException();
        }
    }
}
