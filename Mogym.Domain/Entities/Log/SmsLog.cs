using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities.Log
{
    public class SmsLog:BaseEntity
    {
        public Int64 messageid { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public string statustext { get; set; }
        public string sender { get; set; }
        public string receptor { get; set; }
        public Int64 date { get; set; }
        public int cost { get; set; }

    }
}
