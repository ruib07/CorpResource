using CorpResource.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorpResource.Infrastructure.Configurations;

public class ResourcesConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("Resources");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Type).IsRequired();
        builder.Property(r => r.Status).IsRequired();
        builder.Property(r => r.Description).IsRequired(false).HasColumnType("VARCHAR(MAX)");
        builder.Property(r => r.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(r => r.Department)
               .WithMany(d => d.Resources)
               .HasForeignKey(r => r.DepartmentId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
