using AppTemplate.Domain.DBContexts;
using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Entities.Settings;
using AppTemplate.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.DbContexts
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public DataSeeder(AppDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task SeedAsync()
        {
            await SeedDepartmentsAsync();
            await SeedDesignationsAsync();
            await SeedUserTypesAsync();
            await SeedUsersAsync();
        }

        private async Task SeedDepartmentsAsync()
        {
            if (!_context.Departments.Any())
            {
                _context.Departments.Add(new Department
                {
                    Name = "Software Engineering",
                    CreatedAt = DateTime.Now,
                    StatusId = 1
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedDesignationsAsync()
        {
            if (!_context.Designations.Any())
            {
                _context.Designations.Add(new Designation
                {
                    Name = "Software Engineer",
                    CreatedAt = DateTime.Now,
                    StatusId = 1
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUserTypesAsync()
        {
            if (!_context.UserTypes.Any())
            {
                _context.UserTypes.Add(new UserType
                {
                    Name = "Super Admin",
                    CreatedAt = DateTime.Now,
                    StatusId = 1
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUsersAsync()
        {
            if (!_context.Users.Any())
            {
                var securityStamp = Guid.NewGuid().ToString();
                var passwordHash = _passwordHasher.HashPassword("Admin@123", securityStamp);

                var departmentId = _context.Departments.FirstOrDefault(d => d.Name == "Software Engineering")?.Id ?? 1;
                var designationId = _context.Designations.FirstOrDefault(d => d.Name == "Software Engineer")?.Id ?? 1;
                var userTypeId = _context.UserTypes.FirstOrDefault(ut => ut.Name == "Super Admin")?.Id ?? 1;

                _context.Users.Add(new ApplicationUser
                {
                    FullName = "Super Admin",
                    Email = "admin@mail.com",
                    PhoneNumber = "01511223344",
                    Username = "admin",
                    Password = passwordHash,
                    SecurityStamp = securityStamp,
                    IsLocked = false,
                    IsAdmin = true,
                    LoginTryCount = 0,
                    UserTypeId = userTypeId,
                    DepartmentId = departmentId,
                    DesignationId = designationId,
                    CreatedAt = DateTime.Now,
                    StatusId = 1
                });
                await _context.SaveChangesAsync();
            }
        }
    }

}
