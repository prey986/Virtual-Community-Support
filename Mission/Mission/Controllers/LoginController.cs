using Mission.Entities;
using Mission.Entities.Models;
using Mission.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mission.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(ILoginService loginService, IWebHostEnvironment hostingEnvironment) : ControllerBase
    {
        private readonly ILoginService _loginService = loginService;
        private readonly IWebHostEnvironment _hostingEnvironment = hostingEnvironment;
        ResponseResult result = new ResponseResult();

        [HttpPost]
        [Route("LoginUser")]
        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            try
            {
                result.Data = _loginService.LoginUser(model);
                result.Result = ResponseStatus.Success;
            }
            catch (Exception ex)
            {
                result.Result = ResponseStatus.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(RegisterUserModel model)
        {
            try
            {
                var res = await _loginService.Register(model);
                return Ok(new ResponseResult() { Data = "User Added !", Result = ResponseStatus.Success, Message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = "Failed to add records" });
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public async Task<ActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _loginService.GetUserById(userId);
                return Ok(new ResponseResult() { Data = user, Result = ResponseStatus.Success, Message = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<ActionResult> UpdateUser( UpdateUserModel model)
        {
            try
            {
                var result = await _loginService.UpdateUser(model);
                return Ok(new ResponseResult() { Data = "User Updated Successfully", Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetUserProfileDetailById/{userId}")]
        public async Task<ActionResult> GetUserProfileDetailById(int userId)
        {
            try
            {
                var profile = await _loginService.GetUserProfileDetailById(userId);
                return Ok(new ResponseResult() { Data = profile, Result = ResponseStatus.Success,Message="" });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message=ex.Message});
            }
        }

        [HttpPost]
        [Route("LoginUserProfileUpdate")]
        public async Task<ActionResult> LoginUserProfileUpdate(AddUserDetailsRequestModel model)
        {
            try
            {
                await _loginService.LoginUserProfileUpdate(model);
                return Ok(new ResponseResult() { Data = "Profile Updated Successfully", Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("LoginUserDetailById/{userid}")]
        public async Task<ActionResult> LoginUserDetailById( [FromRoute]int userId)
        {
            try
            {
                var userDetails = await _loginService.GetLoginUserDetailById(userId);
                return Ok(new ResponseResult() { Data = userDetails, Result = ResponseStatus.Success });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseResult() { Data = null, Result = ResponseStatus.Error, Message = ex.Message });
            }
        }
    }
}
