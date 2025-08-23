using CorpResource.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorpResource.Infrastructure.Configurations;

public class AuditLogsConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs");
        builder.HasKey(al => al.Id);
        builder.Property(al => al.Entity).IsRequired();
        builder.Property(al => al.EntityId).IsRequired();
        builder.Property(al => al.Action).IsRequired();
        builder.Property(al => al.PerformedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(al => al.Details).IsRequired(false).HasColumnType("VARCHAR(MAX)");

        builder.HasOne(al => al.PerformedByUser)
               .WithMany(al => al.AuditLogs)
               .HasForeignKey(al => al.PerformedBy)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
