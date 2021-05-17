using FindSelf.Infrastructure.Idempotent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Infrastructure.Database.EntityConfigurations
{
    public class IdempotentRequestEntityTypeConfiguration : IEntityTypeConfiguration<IdempotentRequest>
    {
        public void Configure(EntityTypeBuilder<IdempotentRequest> builder)
        {
            builder.ToTable("IdempotentRequest");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedNever();

            builder.Property(x => x.CommandType).IsRequired();
        }
    }
}
