using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class QuestionsAndAnswersExam
    {
        public string CorrectAnswer { get; set; }
        public string Question { get; set; }
        public int Score { get; set; }
        public string UserAnswer { get; set; }
        public string MediaType { get; set; }
        public string MediaPath { get; set; }
    }
}