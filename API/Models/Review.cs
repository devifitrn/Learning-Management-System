using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("REVIEW")]
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string Contents { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public DateTime ReviewDate { get; set; }
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }
    }
}
