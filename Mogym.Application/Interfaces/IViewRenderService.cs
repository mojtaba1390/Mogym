using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToString(string viewName, object model);
    }

}
