using Mission.Entities.Models;
using System.Threading.Tasks;

namespace Mission.Repositories.IRepositories
{
    public interface ILoginRepository
    {
        LoginUserResponseModel LoginUser(LoginUserRequestModel model);

        Task<string> Register(RegisterUserModel model);

        Task<UserModel> GetUserById(int userId);

        Task<string> UpdateUser(UpdateUserModel model);

        Task<UserProfileDetailsResponse> GetUserProfileDetailById(int userId);

        Task<LoginUserResponseModel> GetLoginUserDetailById(int userId);
        Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel);
    }
}
