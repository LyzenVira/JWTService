using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using JWTService.DAL.Entities;

namespace JWTService.DAL.Context.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            var hasher = new PasswordHasher<Customer>();
            builder.HasData(new  Customer()
            {
                Id = Guid.Parse("24143b4c-87a7-401d-830d-26f8eeaaa43a"),
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                UserName = "Admin",
                FirstName = "Yan",
                LastName = "Nedolia",
                PhoneNumber = "46389589436",
                PasswordHash = hasher.HashPassword(null, "Admin_1"),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
            builder.HasData(new Customer()
            {
                Id = Guid.Parse("1d7f4741-2cb1-4baf-a1f9-65dd95208333"),
                Email = "customer@gmail.com",
                EmailConfirmed = true,
                UserName = "Customer",
                FirstName = "Oleksii",
                LastName = "Bovdur",
                PhoneNumber = "5674474843",
                PasswordHash = hasher.HashPassword(null, "Customer_1"),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            });
        }
    }
}
