using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Views
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string JWT { get; set; }
    }
}
