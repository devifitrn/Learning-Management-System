using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("CATALOGUE")]
    public class Catalogue
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }   
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Course Course { get; set; }
        [JsonIgnore]
        public virtual Category Category { get; set; }
    }
}
