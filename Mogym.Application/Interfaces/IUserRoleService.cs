using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface IUserRoleService
    {
        void Add(UserRole userRole, bool saveChanges);
    }
}
