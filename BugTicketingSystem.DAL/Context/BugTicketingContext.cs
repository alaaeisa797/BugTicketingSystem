using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class BugTicketingContext : IdentityDbContext<CustomUser>
    {
        public BugTicketingContext(DbContextOptions<BugTicketingContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(BugTicketingContext).Assembly
                );
            base.OnModelCreating(modelBuilder);
        }

        #region Tabels 
        public virtual DbSet<Bug> Bugs => Set<Bug>();
        public virtual DbSet<Project> Projects => Set<Project>();
        public virtual DbSet<Attachment> Attachments => Set<Attachment>();
        public virtual DbSet<CustomUser> CustomUsers => Set<CustomUser>();
        public virtual DbSet<UserBug> UserBugs => Set<UserBug>();
      
        #endregion
    }
}
