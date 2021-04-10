using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class UserHistory
    {
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public int Score { get; set; }
        public bool Passed { get; set; }
    }
}