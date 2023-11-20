﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Exercise;
using Mogym.Application.Records.Workout;

namespace Mogym.Application.Interfaces
{
    public interface IWorkoutService
    {
        Task AddOrUpdate(List<WorkoutRecord> workoutRecords);
        Task<ExerciseRecord> GetWorkoutDetails(int id);
    }
}