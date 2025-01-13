using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("QUIZ")]
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public int SubContentId { get; set; }
        public string Question { get; set; }
        [JsonIgnore]
        public virtual SubContent SubContent { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
