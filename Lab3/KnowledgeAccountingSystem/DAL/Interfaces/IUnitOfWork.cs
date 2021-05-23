using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        UserManager<User> UserManagerRepository { get; }
        ICommentRepository CommentRepository { get; }
        IStatusRepository StatusRepository { get; }
        ITagDescriptionRepository TagDescriptionRepository { get; }
        ITagRepository TagRepository { get; }
        IUserRepository UserRepository { get; }

        Task<int> SaveAsync();
    }
}
