﻿using System;
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
        public string Question { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public string Picture { get; set; }

    }
}