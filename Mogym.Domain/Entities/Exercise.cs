﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Domain.Entities
{
    public class Exercise:BaseEntity
    {
        public Exercise()
        {
            ExerciseSets = new HashSet<ExerciseSet>();
            Exercises = new HashSet<Exercise>();
        }

        public int WorkoutId { get; set; }
        public int? SuperSetId { get; set; }


        public int ExerciseVideoId { get; set; }

        public ExerciseVideo ExerciseVideo_Exercise { get; set; }

        public Exercise Exercise_Exercise { get; set; }
        public Workout Exercise_Workout { get; set; }



        public ICollection<ExerciseSet> ExerciseSets { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}
