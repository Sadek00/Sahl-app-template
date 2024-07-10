using AppTemplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Entities.Admin
{
    public class UserType : BaseEntity
    {
        [Required]
        public required string Name { get; set; }        
        public string? Description { get; set; }
    }
}
