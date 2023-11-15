using System;
using System.Collections.Generic;
using System.Text;
using Mogym.Infrastructure.Interfaces;
using Mogym.Infrastructure.Interfaces.Log;

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
        
    }
}
