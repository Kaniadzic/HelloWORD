using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class QuestionsAndAnswers
    {
        public int Number { get; set; }

        public string UserAnswer { get; set; }
        public string CorrectAnswer { get; set; }
    }
}