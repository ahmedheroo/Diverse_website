using Diverse_website.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diverse_website.Models
{
    public class DiverseContext :DbContext
    {

        public DiverseContext()
        {
        }

        public DiverseContext(DbContextOptions<DiverseContext> options)
            : base(options)
        {
        }

        public DbSet<blog> blogs { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<sys_user> sys_Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Diverse_website;Trusted_Connection=True;");

            }
        }

    }
}
