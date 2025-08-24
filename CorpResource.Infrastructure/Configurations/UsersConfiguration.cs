using CorpResource.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorpResource.Infrastructure.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.FullName).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Role).IsRequired();
        builder.Property(u => u.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(u => u.Department)
               .WithMany(d => d.Users)
               .HasForeignKey(u => u.DepartmentId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
