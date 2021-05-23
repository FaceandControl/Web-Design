using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusModel>> GetAllStatuses();
    }
}
