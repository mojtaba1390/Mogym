using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Menu;
using Mogym.Application.Records.Permission;

namespace Mogym.Application.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuRecord>> GetAllActiveMenuList();
        Task<List<PermissionRecord>> GetAllPermissionListForCreateMenuParent();
        Task<MenuRecord> GetMenuByEnglishName(string englishName);
        Task AddMenu(CreateMenuRecord model);
    }
}
