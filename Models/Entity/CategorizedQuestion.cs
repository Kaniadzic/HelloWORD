using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class CategorizedQuestion
    {
        public int Number { get; set; }
        public string Category { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public int Score { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string MediaType { get; set; }
        public string MediaPath { get; set; }
    }
}