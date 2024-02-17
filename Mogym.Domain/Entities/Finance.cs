using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class Finance:BaseEntity
    {
        public int PlanId { set; get; }
        public int TrainingPlanId { set;get;}
        public int? DiscountId { set;get;}
        public double FinalPrice { set;get;}
        public EnumFinanceStatus FinanceStatus { set; get; }


        public Plan Plan_Finance { get; set; }
        public Discount Discount_Finance { get; set; }
    }
}
