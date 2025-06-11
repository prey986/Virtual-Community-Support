using Mission.Services.IServices;
using System;
using System.Threading.Tasks;
using Mission.Repositories.IRepositories;
using Mission.Entities;
using Mission.Entities.Models;
using Mission.Repositories.Helpers;

namespace Mission.Services
{
    public class LoginService(ILoginRepository loginRepository, JwtService jwtService) : ILoginService
    {
        private readonly ILoginRepository _loginRepository = loginRepository;
        private readonly JwtService _jwtService = jwtService;
        ResponseResult result = new ResponseResult();

        public ResponseResult LoginUser(LoginUserRequestModel model)
        {
            var userObj = UserLogin(model);

            if (userObj.Message == "Login Successfully")
            {
                result.Message = userObj.Message;
                result.Result = ResponseStatus.Success;
                result.Data = _jwtService.GenerateToken(userObj.Id.ToString(), userObj.FirstName, userObj.LastName, userObj.PhoneNumber, userObj.EmailAddress, userObj.UserType, userObj.UserImage);
            }
            else
            {
                result.Message = userObj.Message;
                result.Result = ResponseStatus.Error;
            }
            return result;
        }

        public LoginUserResponseModel UserLogin(LoginUserRequestModel model)
        {
            return _loginRepository.LoginUser(model);
        }

        public Task<string> Register(RegisterUserModel model)
        {
            return _loginRepository.Register(model);
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            return await _loginRepository.GetUserById(userId);
        }

        public async Task<string> UpdateUser(UpdateUserModel model)
        {
            return await _loginRepository.UpdateUser(model);
        }

        public async Task<UserProfileDetailsResponse> GetUserProfileDetailById(int userId)
        {
            return await _loginRepository.GetUserProfileDetailById(userId);
        }

        public async Task<LoginUserResponseModel> GetLoginUserDetailById(int userId)
        {
            return await _loginRepository.GetLoginUserDetailById(userId);
        }
        public async Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel)
        {
            return await _loginRepository.LoginUserProfileUpdate(requestModel);
        }
    }
}
