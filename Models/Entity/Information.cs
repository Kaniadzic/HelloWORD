using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloWORD.Models.Entity
{
    [Table("tbl_Informations")]
    public class Information
    {
        [ReadOnly(true)]
        public string Header { get; set; }

        [ReadOnly(true)]
        public string Content { get; set; }
    }
}