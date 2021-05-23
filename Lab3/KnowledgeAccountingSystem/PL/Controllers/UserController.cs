using BLL.Interfaces;
using BLL.Models;
using BLL.Models.In;
using BLL.Models.Out;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<UserPreviewModel>> GetPreviewUsers()
        {
            var PreviewUserModels = await _userService.GetPreviewUsersAsync();
            return Ok(PreviewUserModels);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("by-username/{userName}")]
        public async Task<ActionResult<UserPreviewModel>> GetPreviewUsersByName(string userName)
        {
            var PreviewUserModels = await _userService.GetPreviewUsersByNameAsync(userName);
            return Ok(PreviewUserModels);
        }

        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Roles = "User")]
        [Route("{userId}")]
        public async Task<ActionResult<UserProfileModel>> GetUserProfileById(int userId)
        {
            var ProfileUserModel = await _userService.GetUserProfileByIdAsync(userId);
            return Ok(ProfileUserModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterModel userRegisterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.AddUserAsync(userRegisterModel);

            return Ok();
        }

        [HttpPatch]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        [Route("{userId}")]
        public async Task<ActionResult> UpateUserById([FromRoute] int userId, [FromBody] EditUserModel editUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.UpdateUserAsync(userId, editUserModel);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<UserTokenModel>> Login([FromBody] UserLoginModel userLoginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userWithTokenModel = await _userService.LoginAsync(userLoginModel);

            return Ok(userWithTokenModel);
        }

        [HttpDelete]
        [AllowAnonymous]
        //[Authorize(Roles = "Admin")]
        [Route("{userId}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int userId)
        {
            await _userService.DeleteByIdAsync(userId);
            return Ok();
        }
    }
}
