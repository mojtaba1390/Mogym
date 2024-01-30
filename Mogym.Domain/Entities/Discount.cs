using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Domain.Entities
{
    public class Discount:BaseEntity
    {
        public Discount()
        {
            UserDiscounts = new HashSet<UserDiscount>();
        }
        public string DiscountText { get; set; }
        public EnumDiscountType DiscountType { get; set; }
        public int Value { get; set; }
        public DateTime ActiveDate { get; set; }

        public ICollection<UserDiscount> UserDiscounts { get; set; }

    }
}
