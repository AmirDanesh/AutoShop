using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).ValueGeneratedOnAdd().IsRequired().UseIdentityColumn(1, 1);
            builder.Property(p => p.UserName).IsRequired();
            builder.Property(p => p.Telephone).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Password).IsRequired();

            builder.HasOne(p => p.Role).WithMany(p => p.Users).HasForeignKey(p => p.RoleId);
        }
    }
}
