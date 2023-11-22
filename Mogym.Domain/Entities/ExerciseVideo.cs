using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class ExerciseVideo:BaseEntity
    {
        public ExerciseVideo()
        {
            Exercises = new HashSet<Exercise>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}
