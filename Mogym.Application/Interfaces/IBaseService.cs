using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Common;

namespace Mogym.Application.Interfaces
{
    public interface IBaseService
    {
        Task<ResultDTO?> SendAsync(RequestDTO requestDto);
    }
}
