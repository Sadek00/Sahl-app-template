using AppTemplate.Dto.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Dto.Dtos.Settings
{
    public class DepartmentCreateVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentListVm : CommonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
