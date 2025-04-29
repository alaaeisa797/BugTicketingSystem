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
    public class AttachementConfig : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            // PK
            builder.HasKey(a => a.AttachementId);

            // Properties
            builder.Property(a => a.FilePath)
                   .HasMaxLength(255);

            builder.Property(a => a.FileName)
                   .HasMaxLength(255);

            builder.Property(a => a.FileURL)
                   .IsRequired()
                   .HasMaxLength(500);

          
        }
    }
}
