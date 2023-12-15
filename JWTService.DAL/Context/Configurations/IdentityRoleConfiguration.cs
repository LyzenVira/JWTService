
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JWTService.DAL.Context.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
        {
            builder.HasData(
           new IdentityRole<Guid>()
           {
               Id = Guid.Parse("8379b56f-7881-48ae-bf99-a29f53059332"),
               Name = "Admin",
               NormalizedName = "ADMIN",
               ConcurrencyStamp = Guid.NewGuid().ToString()
           },
           new IdentityRole<Guid>()
           {
               Id = Guid.Parse("25d5bfcb-e10c-49a4-b936-6dd443f23e30"),
               Name = "Customer",
               NormalizedName = "CUSTOMER",
               ConcurrencyStamp = Guid.NewGuid().ToString()
           });
        }
    }
}
