using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Notebook.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {   
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Training> Trainings { get; set; }
    }
}
 