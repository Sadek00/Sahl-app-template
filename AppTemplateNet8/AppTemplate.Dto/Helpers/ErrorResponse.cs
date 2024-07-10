using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Helpers
{
	public class ErrorResponse
	{
		public bool Success { get; set; }
		public string? Message { get; set; }
		public string? Data { get; set; }
	}
}
