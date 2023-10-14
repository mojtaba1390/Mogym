using System;
using System.Collections.Generic;
using System.Text;
using Mogym.Infrastructure.Interfaces;
using Mogym.Infrastructure.Interfaces.Log;

namespace Mogym.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {
        ISeriLogRepository SeriLogRepository { get; }
        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IMenuRepository MenuRepository { get; }
        
    }
}
