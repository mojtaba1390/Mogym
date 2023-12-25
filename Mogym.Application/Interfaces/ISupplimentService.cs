using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Suppliment;

namespace Mogym.Application.Interfaces
{
    public interface ISupplimentService
    {
        Task<List<SupplimentRecord>> GetAll();
        Task AddAsync(SupplimentRecord supplimentRecord);
        Task Edit(int id, string title);
        Task Delete(int deleteId);
    }
}
