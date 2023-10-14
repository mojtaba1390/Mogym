


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
            SeriLogRepository = new SeriLogRepository();

            UserRepository = new UserRepository(_context);
            RoleRepository = new RoleRepository(_context);
            UserRoleRepository = new UserRoleRepository(_context);
            MenuRepository = new MenuRepository(_context);
        }


        public ISeriLogRepository SeriLogRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public IMenuRepository MenuRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
