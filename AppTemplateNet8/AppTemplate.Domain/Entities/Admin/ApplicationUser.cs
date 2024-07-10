using AppTemplate.Domain.Entities.Common;
using AppTemplate.Domain.Entities.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Entities.Admin
{
    public class ApplicationUser : BaseEntityWithoutId
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public string SecurityStamp { get; set; }
        public bool IsLocked { get; set; }
        public int LoginTryCount { get; set; }
        public bool? IsAdmin { get; set; }
        public List<Role>? Role { get; set; }
        [Required]
        public required int UserTypeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }

        [ForeignKey(nameof(UserTypeId))]
        public virtual UserType UserType { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public virtual Department? Department { get; set; }
        [ForeignKey(nameof(DesignationId))]
        public virtual Designation? Designation { get; set; }
    }
}
