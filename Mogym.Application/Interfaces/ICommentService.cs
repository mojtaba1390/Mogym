using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mogym.Application.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAndRate(int planId, string review, int userRating);
    }
}
