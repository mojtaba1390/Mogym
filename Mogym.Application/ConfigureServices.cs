﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Mogym.Application.Interfaces;
using Mogym.Application.Interfaces.ILog;
using Mogym.Application.Services;
using Mogym.Application.Services.Log;
using Mogym.Infrastructure;
using Mogym.Application.Validation.User;
using Mogym.Application.AutoMapper;
using Mogym.Application.AutoMapper.User;
using Mogym.Application.Interfaces.ICache;
using Mogym.Application.Services.Cache;

namespace Mogym.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(LoginRecord_User));


            #region fluent validation
            services.AddControllers()
                .AddFluentValidation(v =>
                {
                    v.ImplicitlyValidateChildProperties = true;
                    v.ImplicitlyValidateRootCollectionElements = true;
                    v.RegisterValidatorsFromAssembly(Assembly.GetAssembly(typeof(LoginValidate)));
                });

            #endregion



            #region Service Life Time
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<ISeriLogService, SeriLogService>();
            services.AddScoped<IUserLoggingService, UserLoggingService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IRedisCacheService, RedisCacheService>();
            services.AddTransient<IPermissionService, PermissionService>();
            services.AddTransient<ITrainerProfileService, TrainerProfileService>();
            services.AddTransient<ITrainerAchievementService, TrainerAchievementService>();
            services.AddTransient<ITrainerPlanCostService, TrainerPlanCostService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IPlanService, PlanService>();
            services.AddTransient<IWorkoutService, WorkoutService>();
            services.AddTransient<IExerciseservice, Exerciseservice>();
            services.AddTransient<IExerciseVideoService, ExerciseVideoService>();
            #endregion

            return services;
        }
    }

}
