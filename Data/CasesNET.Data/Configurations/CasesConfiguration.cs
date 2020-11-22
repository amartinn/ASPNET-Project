namespace CasesNET.Data.Configurations
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using CasesNET.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class CasesConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder
                .HasOne(x => x.Image)
                .WithOne(y => y.Case)
                .HasForeignKey<Image>(c => c.CaseId);
        }
    }
}
