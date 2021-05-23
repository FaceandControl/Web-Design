using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class TagDescriptionRepository : GenericRepository<TagDescription>, ITagDescriptionRepository
    {
        public TagDescriptionRepository(AccountingSystemDbContext context) : base(context)
        {
        }
    }
}
