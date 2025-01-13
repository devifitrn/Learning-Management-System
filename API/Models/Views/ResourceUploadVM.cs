using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class ResourceUploadVM
    {
        public List<IFormFile> File { get; set; }
        public int SubContentId { get; set; }
        public string FileName { get; set; }
        public string Type { get; set; }

    }
}
