using System;
using System.Collections.Generic;
using System.Text;
using Mogym.Infrastructure.Interfaces;

namespace Mogym.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {

        IUserRepository UserRepository { get; }

    }
}
