using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWORD.Models.Entity
{
    public class TrafficQuestion
    {
        public int Number { get; set; }
        public string Question { get; set; }
        public int Score { get; set; }
        public string MediaType { get; set; }
        public string MediaPath { get; set; }
    }
}