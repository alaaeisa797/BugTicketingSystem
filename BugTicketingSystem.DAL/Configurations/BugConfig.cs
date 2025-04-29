using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BugTicketingSystem.DAL.Configurations
{
    public class BugConfig : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
           
            builder.HasKey(b => b.BugId);

          
            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.Description)
                   .HasMaxLength(500);

           
            builder.Property(b => b.Status)
                   .HasConversion<string>() . HasMaxLength(15);
                   

            builder.Property(b => b.Priority)
                   .HasConversion<string>() . HasMaxLength(15);

            //-------------------------------------------1:M with attachement----------------------------------------------------------------------------------//
            builder.HasMany(b => b.Attachments)
                .WithOne(a => a.Bug)
                .HasForeignKey(a => a.Bug_Id)
                .OnDelete(DeleteBehavior.Cascade);
          
        }
    }
}
