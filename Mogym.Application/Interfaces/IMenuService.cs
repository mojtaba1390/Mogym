using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Menu;

namespace Mogym.Application.Interfaces
{
    public interface IMenuService
    {
        Task<List<MenuRecord>> GetAllActiveMenuList();
    }
}
