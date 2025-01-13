using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("SUBCONTENT")]
    public class SubContent
    {
        [Key]
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Title { get; set; }
        public string VideoName { get; set; }
        public DateTime Duration { get; set; }
        [JsonIgnore]
        public virtual Content Content { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
