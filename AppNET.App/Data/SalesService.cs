using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNET.App.Data
{
    public class SalesService:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=FluentAPIDb;Integrated Secrutiy=true;TrustServerCertified=true");
        }
        public DbSet<SalesService> salesServices { get; set; }
        
    }
}
