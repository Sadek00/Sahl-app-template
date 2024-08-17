using AppTemplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Entities.Admin
{
    public class CompanyUser : BaseEntity
    {
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }
        [Required]
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
        public bool? IsLocked { get; set; }
        public int? LoginTryCount { get; set; }
        public bool? IsAdmin { get; set; }
        public List<Role>? Roles { get; set; } = [];
    }
}
