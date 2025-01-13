using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class CourseMasterVM
    {
        public string FirstName { get; set; }
        public string FullName { get; set; }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Features { get; set; }
        public string Status { get; set; }

        public string VideoName { get; set; }
        public DateTime Duration { get; set; }
    }
}
