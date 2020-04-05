using Core.Entities.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.DbMapping.Security
{
    public static class UserMap
    {
        public static ModelBuilder MapUser(this ModelBuilder modelBuilder)
        {
            EntityTypeBuilder<User> entity = modelBuilder.Entity<User>();

            entity.Property(e => e.Email )
                .IsRequired();
            entity.Property(e => e.Name)
                .IsRequired().HasMaxLength(100);
            entity.Property(e => e.UserReference)
                .IsRequired()
                .IsUnicode(false)
                .HasMaxLength(10);

            entity.Property(e => e.DeletedBy).HasMaxLength(10);
            entity.Property(e => e.Job).HasMaxLength(100);
            entity.Property(e => e.Responsable).HasMaxLength(100);
            entity.Property(e => e.Telephone).HasMaxLength(15);
            entity.Property(e => e.Email).HasMaxLength(320);

            entity.HasIndex(e => e.DateDeleted);
            entity.HasIndex(a => a.LanguageId);

            return modelBuilder;
        }
    }
}
