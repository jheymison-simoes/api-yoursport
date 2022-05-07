using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourSport.Business.Models;

namespace YourSport.Data.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnOrder(0);
        builder.Property(u => u.CreatedAt).HasColumnOrder(1);
        builder.Property(u => u.EditedAt).HasColumnOrder(2);
        builder.Property(u => u.DeletedAt).HasColumnOrder(3);
        builder.Property(u => u.Name).IsRequired().HasColumnOrder(4);
        builder.Property(u => u.Email).IsRequired().HasColumnOrder(5);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Key).IsRequired().IsRequired().HasColumnOrder(6);
        builder.Property(u => u.Ddd).HasMaxLength(2).HasColumnOrder(7);
        builder.Property(u => u.PhoneNumber).HasMaxLength(9).HasColumnOrder(8);
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
        builder.Property(u => u.City).IsRequired().HasColumnOrder(9);
        builder.Property(u => u.State).IsRequired().HasColumnOrder(10);
        builder.Property(u => u.StateInitials).HasMaxLength(2).IsRequired().HasColumnOrder(11);
    }
}