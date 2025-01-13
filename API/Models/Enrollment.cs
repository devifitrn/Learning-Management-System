using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("ENROLLMENT")]
    public class Enrollment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int CourseId { get; set; }
        [Required]
        public bool IsComplete { get; set; }
        [Required]
        public PayStatus Status { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        
        public virtual Course Course { get; set; } 
    }
    public enum PayStatus
    {
        Pending,
        Batal,
        Terbayar
    }
}
