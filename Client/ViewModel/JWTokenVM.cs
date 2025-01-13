using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Client.ViewModel
{
    public class JWTokenVM
    {
        public HttpStatusCode Status { get; set; }
        public string JWT { get; set; }
        public string Message { get; set; }
    }
}
