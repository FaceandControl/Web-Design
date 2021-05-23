using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StatusService : DisposableService, IStatusService
    {
        public StatusService(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }

        public async Task<IEnumerable<StatusModel>> GetAllStatuses()
        {
            var statuses = await _unit.StatusRepository.GetAllAsync();

            return statuses.Select(s => new StatusModel { Id = s.Id, UserStatus = s.UserStatus});
        }
    }
}
