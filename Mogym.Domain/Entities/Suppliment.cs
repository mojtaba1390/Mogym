using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Suppliment:BaseEntity
    {
        public Suppliment()
        {
            SupplimentPlans = new HashSet<SupplimentPlan>();
        }
        public string Title { get; set; }




        public ICollection<SupplimentPlan> SupplimentPlans { get; set; }
    }
}
