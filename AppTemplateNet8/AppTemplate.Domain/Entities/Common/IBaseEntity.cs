using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Entities.Common
{
    public abstract class BaseEntityWithoutId
    {
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }        
        public int? StatusId { get; set; }
        public string Identifier { get; set; } = Guid.NewGuid().ToString();
    }
    public abstract class BaseEntityWithoutUpdate
    {
        [Key]
        public int Id { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }        
    }
    public abstract class BaseEntity : BaseEntityWithoutId
    {
        [Key]
        public int Id { get; set; }
    }
    
}
