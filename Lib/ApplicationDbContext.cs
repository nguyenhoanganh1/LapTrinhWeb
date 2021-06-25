using Lib.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
          : base("DefaultConnection")
        {
        }

        public DbSet<Category> Category { get; set; }

    }
}
