using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        public StatusRepository(AccountingSystemDbContext context) : base(context)
        {
        }
    }
}
