using AutoMapper;
using BLL.Interfaces;
using BLL.Models.In;
using BLL.Models.Out;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommentService : DisposableService, ICommentService
    {
        public CommentService(IUnitOfWork unit, IMapper mapper) : base(unit, mapper)
        {
        }

        public async Task AddCommentAsync(AddCommentModel comment, int byUserId) 
        {
            var user = _unit.UserRepository.GetByIdAsync(comment.UserReceiverId);

            if (user == null) 
            {
                throw new AccountingSystemException("User was not found");
            }

            await _unit.CommentRepository.AddAsync(
                new Comment
                {
                    Rating = comment.Rating,
                    Text = comment.Text,
                    UserReceiverId = comment.UserReceiverId,
                    UserSenderId = byUserId,
                    //Tags = comment.Tags
                });
            await _unit.SaveAsync();
        }

        public async Task<double> GetAverageRatingByUserIdAsync(int userId)
        {
            var comments = await _unit.CommentRepository.GetAllAsync();
            if (!comments.Any()) 
            {
                return 0;
            }
            return comments.Where(c => c.UserReceiverId == userId).Average(c => c.Rating);
        }

        public async Task<IEnumerable<CommentByUserModel>> GetCommentsByUserIdAsync(int userId)
        {
            var comments = await _unit.CommentRepository.GetAllAsync();
            return comments.Where(c => c.UserSenderId == userId)
                .Select(c => new CommentByUserModel
                {
                    Rating = c.Rating,
                    Text = c.Text,
                    UserSenderId = c.UserSenderId,
                    UserReceiverId = c.UserReceiverId,
                    ImageLinkUserReceiver = c.UserReceiver.ImageLink,
                    FullNameUserReceiver = c.UserReceiver.FullName,
                    //Tags = c.Tags
                }); 
        }

        public async Task<IEnumerable<CommentForUserModel>> GetCommentsForUserIdAsync(int userId)
        {
            var comments = await _unit.CommentRepository.GetAllAsync();
            return comments.Where(c => c.UserReceiverId == userId)
                .Select(c => new CommentForUserModel
                {
                    Rating = c.Rating,
                    Text = c.Text,
                    UserSenderId = c.UserSenderId,
                    UserReceiverId = c.UserReceiverId,
                    ImageLinkUserSender = c.UserSender.ImageLink,
                    FullNameUserSender = c.UserSender.FullName,
                    //Tags = c.Tags
                });
        }
    }
}
