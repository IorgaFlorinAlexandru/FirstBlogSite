using BlogSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<BlogPost> BlogPost { get; set; }

        public DbSet<BlogSite.Models.PostComment> PostComment { get; set; }

        public DbSet<BlogSite.Models.Message> Message { get; set; }
    }
}
