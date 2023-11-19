using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class ExerciseSet:BaseEntity
    {
        public int? Weight { get; set; }
        public int? Count { get; set; }
        public int? Minute { get; set; }
        public int? Second { get; set; }


        public int ExerciseId { get; set; }


        public Exercise Exercise_ExerciseSet { get; set; }
    }
}
