﻿using System;
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
        IMealRepository MealRepository { get; }
        IIngridientRepository IngridientRepository { get; }
        IMealIngridientRepository MealIngridientRepository { get; }
        ISupplimentRepository SupplimentRepository { get; }
        ISupplimentPlanRepository SupplimentPlanRepository { get; }
        ISupplimentPlanDetailRepository SupplimentPlanDetailRepository { get; }
        IRolePermissionRepository RolePermissionRepository { get; }
        ITicketRepository TicketRepository { get; }
        ITicketDetailRepository TicketDetailRepository { get; }
        ICommentRepository CommentRepository { get; }
        IDiscountRepository DiscountRepository { get; }
        IUserDiscountRepository UserDiscountRepository { get; }
        IDiscountUseRepository DiscountUseRepository { get; }
        IFinanceRepository FinanceRepository { get; }
        ILeadRepository LeadRepository { get; }
        
    }
}
