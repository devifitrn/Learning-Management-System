using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("AUTHORITY")]
    public class Authority
    {
        public string AccountId { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }

        [JsonIgnore]
        public virtual Role Role { get; set; }
    }
}
