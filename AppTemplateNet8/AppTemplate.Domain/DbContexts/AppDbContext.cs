using Microsoft.EntityFrameworkCore;
using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Entities.Settings;
using AppTemplate.Domain.Utilities;
using System.Security.Cryptography;

namespace AppTemplate.Domain.DBContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Admin
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenuPermission> RoleMenuPermissions { get; set; }

        // Settings
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
                

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Data seeding is done using DataSeeder service in Program.cs
        }
    }
}
