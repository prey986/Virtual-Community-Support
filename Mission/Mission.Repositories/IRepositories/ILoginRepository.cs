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

        Task<UserProfileModel> GetUserProfileDetailById(int userId);

        Task<string> UpdateUserProfile(UpdateUserProfileModel model);

        Task<LoginUserResponseModel> GetLoginUserDetailById(int userId);
    }
}
