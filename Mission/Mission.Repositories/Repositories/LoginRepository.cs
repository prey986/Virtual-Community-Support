using Microsoft.EntityFrameworkCore;
using Mission.Entities;
using Mission.Entities.Context;
using Mission.Entities.Models;
using Mission.Entity.Entities;
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

        public async Task<UserProfileDetailsResponse> GetUserProfileDetailById(int userId)
        {
            var user = await _cIDbContext.UserDetails.FirstOrDefaultAsync(u => u.UserId == userId && !u.IsDeleted);
            if (user == null || user.IsDeleted) throw new Exception("User not found");
            return new UserProfileDetailsResponse
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                EmployeeId = user.EmployeeId,
                Manager = user.Manager,
                Title = user.Title,
                Department = user.Department,
                MyProfile = user.MyProfile,
                WhyIVolunteer = user.WhyIVolunteer,
                CountryId = user.CountryId,
                CityId = user.CityId,
                Avilability = user.Availability,
                LinkdInUrl = user.LinkedInUrl,
                MySkills = user.MySkills, 
                UserImage = user.UserImage,
                UserId = user.UserId
            };
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
        public async Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel)
        {
            try
            {
                var user = _cIDbContext.User.Where(x => x.Id == requestModel.UserId).FirstOrDefault();

                if (user == null) throw new Exception("Not Found!");

                var userDetails = _cIDbContext.UserDetails.Where(x => x.UserId == requestModel.UserId).FirstOrDefault();

                if (userDetails == null)
                {
                    // Add User Details
                    UserDetail userDetail = new UserDetail()
                    {
                        UserId = requestModel.UserId,
                        Availability = requestModel.Avilability,
                        CityId = requestModel.CityId,
                        CountryId = requestModel.CountryId,
                        Department = requestModel.Department,
                        EmployeeId = requestModel.EmployeeId,
                        LinkedInUrl = requestModel.LinkdInUrl,
                        Manager = requestModel.Manager,
                        MyProfile = requestModel.MyProfile,
                        MySkills = requestModel.MySkills,
                        Surname = requestModel.Surname,
                        Name = requestModel.Name,
                        UserImage = requestModel.UserImage,
                        WhyIVolunteer = requestModel.WhyIVolunteer,
                        Status = requestModel.Status,
                        Title = requestModel.Title,

                        IsDeleted = false,
                        CreatedDate = DateTime.Now,
                    };

                    await _cIDbContext.UserDetails.AddAsync(userDetail);
                }
                else
                {
                    // Update User Details
                    userDetails.UserId = requestModel.UserId;
                    userDetails.Availability = requestModel.Avilability;
                    userDetails.CityId = requestModel.CityId;
                    userDetails.CountryId = requestModel.CountryId;
                    userDetails.Department = requestModel.Department;
                    userDetails.EmployeeId = requestModel.EmployeeId;
                    userDetails.LinkedInUrl = requestModel.LinkdInUrl;
                    userDetails.Manager = requestModel.Manager;
                    userDetails.MyProfile = requestModel.MyProfile;
                    userDetails.MySkills = requestModel.MySkills;
                    userDetails.Surname = requestModel.Surname;
                    userDetails.Name = requestModel.Name;
                    userDetails.UserImage = requestModel.UserImage;
                    userDetails.WhyIVolunteer = requestModel.WhyIVolunteer;
                    userDetails.Status = requestModel.Status;
                    userDetails.Title = requestModel.Title;

                    userDetails.ModifiedDate = DateTime.Now;

                    _cIDbContext.UserDetails.Update(userDetails);
                }

                user.FirstName = requestModel.Name;
                user.LastName = requestModel.Surname;

                _cIDbContext.User.Update(user);
                await _cIDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
