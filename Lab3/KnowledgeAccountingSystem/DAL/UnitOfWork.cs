using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountingSystemDbContext _context;

        private ICommentRepository _commentRepository;
        private IStatusRepository _statusRepository;
        private ITagDescriptionRepository _tagDescriptionRepository;
        private ITagRepository _tagRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(AccountingSystemDbContext context, UserManager<User> userManagerRepository)
        {
            _context = context;
            UserManagerRepository = userManagerRepository;
        }

        public UserManager<User> UserManagerRepository { get; }

        public ICommentRepository CommentRepository =>
            _commentRepository ??= new CommentRepository(_context);

        public IStatusRepository StatusRepository =>
            _statusRepository ??= new StatusRepository(_context);

        public ITagDescriptionRepository TagDescriptionRepository =>
            _tagDescriptionRepository ??= new TagDescriptionRepository(_context);

        public ITagRepository TagRepository =>
            _tagRepository ??= new TagRepository(_context);

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_context);

        public async Task<int> SaveAsync()
        {
            OnBeforeSaving();
            return await _context.SaveChangesAsync();
        }

        private void OnBeforeSaving()
        {
            var entries = _context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    var now = DateTime.Now;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastModified = now;
                            break;

                        case EntityState.Added:
                            trackable.DateCreated = now;
                            trackable.LastModified = now;
                            trackable.IsDeleted = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            trackable.LastModified = now;
                            trackable.IsDeleted = true;
                            break;
                    }
                }
            }
        }

        private bool _isDisposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
