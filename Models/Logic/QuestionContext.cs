using HelloWORD.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Logic
{
    public class QuestionContext
    {
        public DbSet<Questions> Questions { get; set; }
    }
}