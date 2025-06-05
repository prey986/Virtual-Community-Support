using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Repositories.IRepositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mission.Repositories
{
    public class LoginRepository(MissionDbContext cIDbContext) : ILoginRepository
    {
        private readonly MissionDbContext _cIDbContext = cIDbContext;

        public LoginUserResponseModel LoginUser(LoginUserRequestModel model)
        {
            var existingUser = _cIDbContext.User
                .FirstOrDefault(u => u.EmailAddress.ToLower() == model.EmailAddress.ToLower() && !u.IsDeleted);

            if (existingUser == null)
            {
                return new LoginUserResponseModel() { Message = "Email Address Not Found." };
            }

            if (existingUser.Password != model.Password)
            {
                return new LoginUserResponseModel() { Message = "Incorrect Password." };
            }

            return new LoginUserResponseModel
            {
                Id = existingUser.Id,
                FirstName = existingUser.FirstName ?? string.Empty,
                LastName = existingUser.LastName ?? string.Empty,
                PhoneNumber = existingUser.PhoneNumber,
                EmailAddress = existingUser.EmailAddress,
                UserType = existingUser.UserType,
                UserImage = existingUser.UserImage ?? string.Empty,
                Message = "Login Successfully"
            };
        }

        public async Task<string> Register(RegisterUserModel model)
        {
            var isExist = _cIDbContext.User
                .FirstOrDefault(x => x.EmailAddress == model.EmailAddress && !x.IsDeleted);

            if (isExist != null) throw new Exception("Email already exists");

            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                UserType = "user",
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow
            };

            await _cIDbContext.User.AddAsync(user);
            await _cIDbContext.SaveChangesAsync();
            return "User Added!";
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            var user = await _cIDbContext.User.FindAsync(userId);
            if (user == null || user.IsDeleted) return null;

            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
                UserImage = user.UserImage
            };
        }

        public async Task<string> UpdateUser(UpdateUserModel model)
        {
            var user = await _cIDbContext.User.FindAsync(model.Id);
            if (user == null || user.IsDeleted) throw new Exception("User not found");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.EmailAddress = model.EmailAddress;
            user.PhoneNumber = model.PhoneNumber;
            user.UserType = model.UserType;

            _cIDbContext.User.Update(user);
            await _cIDbContext.SaveChangesAsync();

            return "User updated successfully";
        }

        public async Task<UserProfileModel> GetUserProfileDetailById(int userId)
        {
            var user = await _cIDbContext.User.FindAsync(userId);
            if (user == null || user.IsDeleted) return null;

            return new UserProfileModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.EmailAddress,
                UserImage = user.UserImage
            };
        }

        public async Task<string> UpdateUserProfile(UpdateUserProfileModel model)
        {
            var user = await _cIDbContext.User.FindAsync(model.Id);
            if (user == null || user.IsDeleted) throw new Exception("User not found");

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.UserImage = model.UserImage;

            _cIDbContext.User.Update(user);
            await _cIDbContext.SaveChangesAsync();

            return "Profile updated successfully";
        }

        public async Task<LoginUserResponseModel> GetLoginUserDetailById(int userId)
        {
            var user = await _cIDbContext.User.FindAsync(userId);
            if (user == null || user.IsDeleted) return null;

            return new LoginUserResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                PhoneNumber = user.PhoneNumber,
                UserType = user.UserType,
                UserImage = user.UserImage,
                Message = "User fetched successfully"
            };
        }
    }
}
