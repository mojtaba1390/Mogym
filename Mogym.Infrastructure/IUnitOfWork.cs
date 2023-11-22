using System;
using System.Collections.Generic;
using System.Text;
using Mogym.Infrastructure.Interfaces;
using Mogym.Infrastructure.Interfaces.Log;
using Mogym.Infrastructure.Repositories;

namespace Mogym.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IMenuRepository MenuRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        ITrainerProfileRepository TrainerProfileRepository { get; }
        ITrainerAchievementRepository TrainerAchievementRepository { get; }
        ITrainerPlanCostRepository TrainerPlanCostRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IPlanRepository PlanRepository { get; }
        IWorkoutRepository WorkoutRepository { get; }
        IExerciseRepository ExerciseRepository { get; }
        IExerciseVideoRepository ExerciseVideoRepository { get; }
        IExerciseSetRepository ExerciseSetRepository { get; }
        
    }
}
