using Core5Ng.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Core5Ng.Core.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
               new { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR", RoleName = "Administrator", Handle = "administrator", RoleIcon = "/uploads/roles/icons/default/role.png", IsActive = true },
               new { Id = "2", Name = "Customer", NormalizedName = "CUSTOMER", RoleName = "customer", Handle = "customer", RoleIcon = "/uploads/roles/icons/default/role.png", IsActive = true }
             );

            builder.Entity<ApplicationUser>().HasKey(k => k.Id);
            builder.Entity<ApplicationUser>().HasMany(i => i.Address).WithOne(s => s.ApplicationUser);

            builder.Entity<Address>().HasKey(v => v.AddressId);
            builder.Entity<Address>().HasOne(v => v.ApplicationUser).WithMany(l=> l.Address);

            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
