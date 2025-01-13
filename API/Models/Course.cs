using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("COURSE")]
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Features { get; set; }
        public string Picture { get; set; }

        public Status Status { get; set; }
        public string Feedback { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Catalogue> Catalogues { get; set; }
        [JsonIgnore]
        public virtual ICollection<Content> Contents { get; set; }
        [JsonIgnore]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }

    }
    public enum Status
    {
        Incomplete,
        Review,
        Revise,
        Approve
    }
}
