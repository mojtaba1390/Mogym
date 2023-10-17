using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Permission;

namespace Mogym.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<PermissionRecord> GetPermissionByEnglishName(string englishName);
        Task<List<PermissionRecord>> GetAll();
    }
}
