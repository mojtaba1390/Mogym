using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Records.Profile
{
    public record TrainerPaymentRecord
    {
        public string CartNumber { get; init; }
        public string Cost { get; set; }
        public string CartOwner { get; init; }
    }
}
