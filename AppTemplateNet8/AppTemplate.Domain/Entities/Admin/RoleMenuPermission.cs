using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AppTemplate.Domain.Entities.Common;

namespace AppTemplate.Domain.Entities.Admin
{
    public class RoleMenuPermission : BaseEntity
	{
		[Required]
		public int RoleId { get; set; }

		[Required]
		public int MenuId { get; set; }


		[ForeignKey(nameof(MenuId))]
		public virtual Menu? Menu { get; set; }

		[ForeignKey(nameof(RoleId))]
		public virtual Role? Role { get; set; }
	}
}
