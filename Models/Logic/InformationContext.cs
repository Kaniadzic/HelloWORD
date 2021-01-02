using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using HelloWORD.Models.Entity;
using System.Data.Entity;

// To jest chyba niepotrzebne
namespace HelloWORD.Models.Logic
{
    public class InformationContext : DbContext
    {
        public DbSet<Information> Informations { get; set; }
    }
}