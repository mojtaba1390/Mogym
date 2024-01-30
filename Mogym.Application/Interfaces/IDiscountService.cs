using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Interfaces
{
    public interface IDiscountService
    {
        Task<Tuple<int,string,string>> GetDiscountPrice(string discountText, int trainingPlanId);
    }
}
