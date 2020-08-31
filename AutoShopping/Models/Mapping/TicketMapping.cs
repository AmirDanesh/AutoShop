using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShopping.Models.Mapping
{
    public class TicketMapping : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.Title).IsRequired();
            builder.Property(p => p.StatusId).IsRequired();
            builder.Property(p => p.CreateDate).IsRequired();

            builder.HasOne(p => p.Status).WithMany(p => p.Tickets).HasForeignKey(p => p.StatusId);
            builder.HasMany(p => p.TicketDetails).WithOne(p => p.Ticket).HasForeignKey(p => p.TicketId);
        }
    }
}
