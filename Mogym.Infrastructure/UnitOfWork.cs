


using Mogym.Domain.Context;
using Mogym.Infrastructure.Interfaces;
using Mogym.Infrastructure.Repositories;

namespace Mogym.Infrastructure
{
    public class UnitOfWork :IUnitOfWork
    {

        protected readonly MogymContext _context;
        public UnitOfWork(MogymContext context)
        {
            _context = context;
            UserRepository = new UserRepository(_context);

        }




        public IUserRepository UserRepository { get; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
