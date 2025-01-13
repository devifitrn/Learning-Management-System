using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("RESOURCE")]
    public class Resource
    {
        [Key]
        public int Id { get; set; }
        public int SubContentId { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public virtual SubContent SubContent { get; set; }
    }
}
