using AutoMapper;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using BLL.Validation;
using System;
using System.Text;
using BLL.Configs;
using BLL.Models.Out;
using BLL.Models.In;

namespace BLL.Services
{
    public class UserService : DisposableService, IUserService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ICommentService _commentService;

        public UserService(IUnitOfWork unit, IMapper mapper, IOptions<JwtOptions> jwtOptions, ICommentService commentService)
            : base(unit, mapper)
        {
            _jwtOptions = jwtOptions.Value;
            _commentService = commentService;
        }

        public async Task AddUserAsync(UserRegisterModel registerModel)
        {
            if (await _unit.UserManagerRepository.FindByEmailAsync(registerModel.Email) != null)
            {
                throw new AccountingSystemException("User with same email already exists");
            }
            var user = _mapper.Map<User>(registerModel);
            user.StatusId = -4;
            await _unit.UserManagerRepository.CreateAsync(user, registerModel.Password);
            await _unit.UserManagerRepository.AddToRoleAsync(user, "User");
        }

        public async Task UpdateUserAsync(int userId, EditUserModel editUserModel) 
        {
            var user = await _unit.UserManagerRepository.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new AccountingSystemException("User was not found");
            }

            _mapper.Map<EditUserModel, User>(editUserModel, user);
            await _unit.UserManagerRepository.UpdateAsync(user);
        }

        public async Task DeleteByIdAsync(int userId)
        {
            var user = await _unit.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new AccountingSystemException("User was not found");
            }

            await _unit.UserManagerRepository.DeleteAsync(user);
        }

        public async Task<IEnumerable<UserPreviewModel>> GetPreviewUsersAsync()
        {
            var users = await _unit.UserRepository.GetAllAsync();
            if (users == null) 
            {
                return new List<UserPreviewModel>();
            }
            return _mapper.Map<IEnumerable<UserPreviewModel>>(users)
                .Select(u => { u.Rating = _commentService.GetAverageRatingByUserIdAsync(u.Id).Result; return u; });
        }

        public async Task<IEnumerable<UserPreviewModel>> GetPreviewUsersByNameAsync(string userName)
        {
            var users = await _unit.UserRepository.GetAllAsync();
            if (users == null)
            {
                return new List<UserPreviewModel>();
            }
            return _mapper.Map<IEnumerable<UserPreviewModel>>(users
                .Where(u => u.FullName.ToLower().Contains(userName.ToLower())))
                .Select(u => { u.Rating = _commentService.GetAverageRatingByUserIdAsync(u.Id).Result; return u; });
        }

        public async Task<UserProfileModel> GetUserProfileByIdAsync(int userId)
        {
            var user = await _unit.UserRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new AccountingSystemException("User was not found");
            }
            var userProfileModel = _mapper.Map<UserProfileModel>(user);
            userProfileModel.Rating = await _commentService.GetAverageRatingByUserIdAsync(userProfileModel.Id);
            return userProfileModel;
        }

        public async Task<UserTokenModel> LoginAsync(UserLoginModel loginModel)
        {
            var user = await _unit.UserManagerRepository.FindByEmailAsync(loginModel.Email);

            if (user == null || !await _unit.UserManagerRepository.CheckPasswordAsync(user, loginModel.Password))
            {
                throw new AccountingSystemException("Incorrect email or password");
            }

            var token = GenerateJwtToken(user);
            var role = _unit.UserManagerRepository.GetRolesAsync(user).Result.First();

            return _mapper.Map<UserTokenModel>(user, opt => { opt.Items["role"] = role; opt.Items["token"] = token; });
        }

        private string GenerateJwtToken(User user) 
        {
            var claims = _unit.UserManagerRepository.GetRolesAsync(user).Result.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            var expires = DateTime.UtcNow.Add(_jwtOptions.LifeTime);

            var key = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
