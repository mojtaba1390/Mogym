using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Lead:BaseEntity
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
    }
}
