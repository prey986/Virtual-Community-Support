namespace Mission.Entities.Models
{
    public class UpdateUserProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImage { get; set; }
    }
}