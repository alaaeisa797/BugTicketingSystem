using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTicketingSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.Configurations
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.ProjectId);
            builder.HasMany(p => p.Bugs)
                .WithOne(b => b.Project)
                .HasForeignKey(b => b.Project_Id)
                .OnDelete(DeleteBehavior.Cascade); // just in case i want to delete the projetc .. its bugs will be deleeted aslo

            builder.Property(p => p.ProjectName).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(200);

        }
    }
}
