﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("ROLE")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Authority> Authorities { get; set; }
    }
}
