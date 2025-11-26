using EcommerceWebApp.Entities.Entities.Products;
using EcommerceWebApp.Entities.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceWebApp.Entities.Mapping.Inventory
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.ContactNumber)
                .HasMaxLength(20);

            builder.Property(u => u.ImageURL)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(u => u.SecurityQuestion)
                .HasMaxLength(200);

            builder.Property(u => u.SecurityAnswer)
                .HasMaxLength(200);

            builder.Property(u => u.CreatedDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(u => u.LastModifiedDate)
                .HasColumnType("datetime2");

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
