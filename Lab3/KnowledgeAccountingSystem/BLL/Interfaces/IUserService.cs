using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using BLL.Models.In;
using BLL.Models.Out;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(UserRegisterModel registerModel);
        Task UpdateUserAsync(int userId, EditUserModel editUserModel);
        Task<IEnumerable<UserPreviewModel>> GetPreviewUsersAsync();
        Task<IEnumerable<UserPreviewModel>> GetPreviewUsersByNameAsync(string userName);
        Task<UserProfileModel> GetUserProfileByIdAsync(int userId);
        Task<UserTokenModel> LoginAsync(UserLoginModel loginModel);
        Task DeleteByIdAsync(int userId);
    }
}
