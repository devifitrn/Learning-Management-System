using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class QuizPostVM
    {
        public int SubContentId { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public bool IsCorrect1 { get; set; }
        public string Answer2 { get; set; }
        public bool IsCorrect2 { get; set; }
        public string Answer3 { get; set; }
        public bool IsCorrect3 { get; set; }
        public string Answer4 { get; set; }
        public bool IsCorrect4 { get; set; }
    }
}
