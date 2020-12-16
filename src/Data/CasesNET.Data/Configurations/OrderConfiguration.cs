namespace CasesNET.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CasesNET.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order
             .HasOne(c => c.Cart)
             .WithOne(o => o.Order)
             .HasForeignKey<Order>()
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
