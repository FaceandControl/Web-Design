using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
    }
}
