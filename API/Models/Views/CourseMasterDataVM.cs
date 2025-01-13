using System;
using System.Collections.Generic;

namespace API.Models.Views
{
    public class CourseMasterDataVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Features { get; set; }
        public string Status { get; set; }
        public string Picture { get; set; }
        public string Feedback { get; set; }
        public User User { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Content> Contents { get; set; }
    }
}
