using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("CONTENT")]
    public class Content
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<SubContent> SubContent { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }
    }
}
