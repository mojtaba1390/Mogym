


using Mogym.Domain.Context;
using Mogym.Infrastructure.Interfaces;
using Mogym.Infrastructure.Interfaces.Log;
using Mogym.Infrastructure.Repositories;
using Mogym.Infrastructure.Repositories.Log;

namespace Mogym.Infrastructure
{
    public class UnitOfWork :IUnitOfWork
    {

        protected readonly MogymContext _context;
        public UnitOfWork(MogymContext context)
        {
            _context = context;

            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
            UserRoleRepository = new UserRoleRepository(_context);
            MenuRepository = new MenuRepository(_context);
            PermissionRepository = new PermissionRepository(_context);
            TrainerProfileRepository = new TrainerProfileRepository(_context);
            TrainerAchievementRepository = new TrainerAchievementRepository(_context);
            TrainerPlanCostRepository = new TrainerPlanCostRepository(_context);
            QuestionRepository = new QuestionRepository(_context);
            PlanRepository = new PlanRepository(_context);
            WorkoutRepository = new WorkoutRepository(_context);
            ExerciseRepository = new ExerciseRepository(_context);
            ExerciseVideoRepository = new ExerciseVideoRepository(_context);
            ExerciseSetRepository = new ExerciseSetRepository(_context);
            MealRepository = new MealRepository(_context);
            IngridientRepository = new IngredientRepository(_context);
            MealIngridientRepository = new MealIngridientRepository(_context);
            SupplimentRepository = new SupplimentRepository(_context);
            SupplimentPlanRepository = new SupplimentPlanRepository(_context);
            SupplimentPlanDetailRepository = new SupplimentPlanDetailRepository(_context);
            RolePermissionRepository = new RolePermissionRepository(_context);
            TicketRepository = new TicketRepository(_context);
            TicketDetailRepository = new TicketDetailRepository(_context);
            CommentRepository = new CommentRepository(_context);
            DiscountRepository = new DiscountRepository(_context);
            UserDiscountRepository = new UserDiscountRepository(_context);
            DiscountUseRepository=new DiscountUseRepository(_context);
            FinanceRepository=new FinanceRepository(_context);
        }


        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IMenuRepository MenuRepository { get; }
        public IPermissionRepository PermissionRepository { get; }
        public ITrainerProfileRepository TrainerProfileRepository { get; }
        public ITrainerAchievementRepository TrainerAchievementRepository { get; }
        public ITrainerPlanCostRepository TrainerPlanCostRepository { get; }
        public IQuestionRepository QuestionRepository { get; }
        public IPlanRepository PlanRepository { get; }
        public IWorkoutRepository WorkoutRepository { get; }
        public IExerciseRepository ExerciseRepository { get; }
        public IExerciseVideoRepository ExerciseVideoRepository { get; }
        public IExerciseSetRepository ExerciseSetRepository { get; }
        public IMealRepository MealRepository { get; }
        public IIngridientRepository IngridientRepository { get; }
        public IMealIngridientRepository MealIngridientRepository { get; }
        public ISupplimentRepository SupplimentRepository { get; }
        public ISupplimentPlanRepository SupplimentPlanRepository { get; }
        public ISupplimentPlanDetailRepository SupplimentPlanDetailRepository { get; }
        public IRolePermissionRepository RolePermissionRepository { get; }
        public ITicketRepository TicketRepository { get; }
        public ITicketDetailRepository TicketDetailRepository { get; }
        public ICommentRepository CommentRepository { get; }
        public IDiscountRepository DiscountRepository { get; }
        public IUserDiscountRepository UserDiscountRepository { get; }
        public IDiscountUseRepository DiscountUseRepository { get; }
        public IFinanceRepository FinanceRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
