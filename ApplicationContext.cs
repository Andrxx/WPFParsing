using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WPFParsing.Models;

namespace WPFParsing
{
    internal class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlite("Data Source=ParseDB.db");
        }
        public DbSet<Contractor> Contractors { get; set; } = null!;
    }
}
