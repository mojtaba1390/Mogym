using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Permission;
using Mogym.Domain.Entities;

namespace Mogym.Application.Interfaces
{
    public interface IPermissionService
    {
        Task<PermissionRecord> GetPermissionByEnglishName(string englishName);
        Task<List<PermissionRecord>> GetAll();
        Task<PermissionRecord> AddAsync(Permission permission, bool saveChanges);
        Task<List<PermissionToRolePermissionRecord>> GetAllForRolePermission();
    }
}
