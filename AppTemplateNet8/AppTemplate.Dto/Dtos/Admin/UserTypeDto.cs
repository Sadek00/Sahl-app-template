using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppTemplate.Dto.Dtos.Common;

namespace AppTemplate.Dto.Dtos.Admin
{
    public class UserTypeCreateVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UserTypeUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UserTypeListVm : CommonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}