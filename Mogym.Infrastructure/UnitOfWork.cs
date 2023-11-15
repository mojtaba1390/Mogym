


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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
