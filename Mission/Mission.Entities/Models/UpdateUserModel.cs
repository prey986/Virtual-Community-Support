using Microsoft.AspNetCore.Http;

namespace Mission.Entities.Models
{
    public class UpdateUserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string UserType { get; set; }
        public IFormFile? ProfileImage { get; set; }  // Must match form key 'profileImage'
    }
}