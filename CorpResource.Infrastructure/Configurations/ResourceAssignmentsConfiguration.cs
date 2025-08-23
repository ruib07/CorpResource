using CorpResource.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorpResource.Infrastructure.Configurations;

public class ResourceAssignmentsConfiguration : IEntityTypeConfiguration<ResourceAssignment>
{
    public void Configure(EntityTypeBuilder<ResourceAssignment> builder)
    {
        builder.ToTable("ResourceAssignments");
        builder.HasKey(ra => ra.Id);
        builder.Property(ra => ra.AssignedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(ra => ra.ReturnedAt).IsRequired(false);
        builder.Property(ra => ra.Status).IsRequired();
        builder.Property(ra => ra.Notes).IsRequired(false).HasColumnType("VARCHAR(MAX)");

        builder.HasOne(ra => ra.Resource)
               .WithMany(r => r.ResourceAssignments)
               .HasForeignKey(ra => ra.ResourceId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ra => ra.User)
               .WithMany(u => u.ResourceAssignments)
               .HasForeignKey(ra => ra.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
