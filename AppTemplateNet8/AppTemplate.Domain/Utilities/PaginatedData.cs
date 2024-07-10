using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Domain.Utilities
{
    public class PaginatedData<T>
    {
        public List<T>? Data { get; set; }
        public int? TotalRows { get; set; }
    }
}
