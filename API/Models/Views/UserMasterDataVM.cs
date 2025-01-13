using System;

namespace API.Models.Views
{
    public class UserMasterDataVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Roles { get; set; }
        public UserStatus Status { get; set; }
    }
}

