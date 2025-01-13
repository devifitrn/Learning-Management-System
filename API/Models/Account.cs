using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("ACCOUNT")]
    public class Account
    {
        [Key]
        public string Id { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredToken { get; set; }
        public bool IsUsed { get; set; }
        public string OTP { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Authority> Authorities { get; set; }

    }
}
