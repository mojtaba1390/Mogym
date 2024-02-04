using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mogym.Application.Records.Lead;

namespace Mogym.Application.Interfaces
{
    public interface ILeadService
    {
        Task Submit(LeadRecord leadRecord);
    }
}
