using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
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
using Mogym.Common.ModelExtended;

namespace Mogym.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(LoginRecord_User));


            //var configuration = new ConfigurationBuilder()
            //    .AddJsonFile($"appsettings.json");

            //var config = configuration.Build();
            //var emailConfig = services.Configure<Email>(config.GetSection("EmailConfiguration"));
            //services.AddSingleton(emailConfig);

            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddHttpClient<ISmsService, SmsService>();

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
            services.AddScoped<IBaseService, BaseService>();
            services.AddScoped<ISmsService, SmsService>();
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
            services.AddTransient<IExerciseSetService, ExerciseSetService>();
            services.AddTransient<IMealService, MealService>();
            services.AddTransient<IIngridientService, IngridientService>();
            services.AddTransient<IMealIngridientService, MealIngridientService>();
            services.AddTransient<ISupplimentService, SupplimentService>();
            services.AddTransient<ISupplimentPlanService, SupplimentPlanService>();
            services.AddTransient<ISupplimentPlanDetailService, SupplimentPlanDetailService>();
            services.AddTransient<ISmsLogService, SmsLogService>();
            services.AddTransient<IRolePermissionService, RolePermissionService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITicketDetailService, TicketDetailService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IUserDiscountService, UserDiscountService>();
            services.AddTransient<IFinanceService, FinanceService>();
            services.AddTransient<ILeadService, LeadService>();

            #endregion




            services.AddHttpClient();



            return services;
        }
    }

}
