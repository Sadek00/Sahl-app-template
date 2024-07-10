using AppTemplate.Dto.Dtos.Common;

namespace AppTemplate.Dto.Dtos.Settings
{
    public class DesignationCreateVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DesignationUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class DesignationListVm : CommonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
