using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.Phone).HasMaxLength(11);
        builder.Property(x => x.Name).HasMaxLength(20);
        builder.Property(x => x.LastName).HasMaxLength(20);
        
    }
}
