using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("USER")]
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePicture { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }
        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }
        [JsonIgnore]
        public virtual ICollection<Course> Courses { get; set; }

    }
    public enum Gender
    {
        Male,
        Female,
    }
    public enum UserStatus
    {
        Normal,
        Request,
        Approve
    }
}
