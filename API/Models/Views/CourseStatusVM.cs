using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class CourseStatusVM
    {
        public int Id { get; set; }
        public int Status { get; set; }
    }
}
