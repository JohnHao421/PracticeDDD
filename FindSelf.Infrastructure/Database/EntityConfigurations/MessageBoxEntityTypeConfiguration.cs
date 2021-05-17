using FindSelf.Domain.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindSelf.Infrastructure.Database.EntityConfigurations
{
    public class MessageBoxEntityTypeConfiguration : IEntityTypeConfiguration<MessageBox>
    {
        public void Configure(EntityTypeBuilder<MessageBox> builder)
        {
            builder.ToTable("MessageBox");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();

            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<MessageBox>(box => box.Uid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class SiteMessageEntityTypeConfiguration : IEntityTypeConfiguration<SiteMessage>
    {
        public void Configure(EntityTypeBuilder<SiteMessage> builder)
        {
            builder.ToTable("SiteMessage");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().IsRequired();

            builder.HasOne<MessageBox>().WithMany(x => x.Messages).HasForeignKey("BoxId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.ReceiverId).IsRequired();
            builder.Property(x => x.SnederId).IsRequired();
            builder.Property(x => x.SendTimesteamp).IsRequired();
        }
    }
}
