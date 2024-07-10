using AppTemplate.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace AppTemplate.Domain.Entities.Settings
{
    public class Designation : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
