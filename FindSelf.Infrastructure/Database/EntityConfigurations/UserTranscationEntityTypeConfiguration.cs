using FindSelf.Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Infrastructure.Database.EntityConfigurations
{
    public class UserTranscationEntityTypeConfiguration : IEntityTypeConfiguration<UserTranscation>
    {
        public void Configure(EntityTypeBuilder<UserTranscation> builder)
        {
            builder.ToTable("UserTranscation");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.CreateTime).IsRequired();
        }
    }
}
