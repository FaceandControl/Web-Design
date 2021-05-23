using BLL.Models.In;
using BLL.Models.Out;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommentService
    {
        public Task<double> GetAverageRatingByUserIdAsync(int userId);
        public Task<IEnumerable<CommentByUserModel>> GetCommentsByUserIdAsync(int userId);
        Task<IEnumerable<CommentForUserModel>> GetCommentsForUserIdAsync(int userId);
        public Task AddCommentAsync(AddCommentModel comment, int byUserId);
    }
}
