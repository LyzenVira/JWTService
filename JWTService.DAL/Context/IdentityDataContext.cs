
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection;
using JWTService.DAL.Entities;

namespace JWTService.DAL.Context
{
    public class IdentityDataContext : IdentityDbContext<Customer, IdentityRole<Guid>, Guid>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
