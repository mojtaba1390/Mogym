using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class UserDiscount:BaseEntity
    {
        public int UserId { get; set; }

        public int DiscountId { get; set; }


        public User UserDiscount_User { get; set; }

        public Discount UserDiscount_Discount { get; set; }
    }
}
