using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestTest.Domain.Entities;

namespace RestTest.Infrastructure.Data.Configurations;
public class ResourceItemConfiguration : IEntityTypeConfiguration<ResourceItem>
{
    public void Configure(EntityTypeBuilder<ResourceItem> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(t => t.Location)
            .HasMaxLength(200)
            .IsRequired();
    }
}
