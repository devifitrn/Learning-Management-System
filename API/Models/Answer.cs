using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("ANSWER")]
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Contents { get; set; }
        public bool IsCorrect { get; set; }
        [JsonIgnore]
        public virtual Quiz Quiz { get; set; }
    }
}
