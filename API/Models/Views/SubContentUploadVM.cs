using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class SubContentUploadVM
    {
        public IFormFile Video { get; set; }
        public int ContentId { get; set; }
        public string Title { get; set; }
        public string VideoName { get; set; }
        public DateTime Duration { get; set; }

    }
}
