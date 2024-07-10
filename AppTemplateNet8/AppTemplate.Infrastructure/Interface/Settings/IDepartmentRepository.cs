using AppTemplate.Domain.Entities.Settings;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Settings;
using AppTemplate.Infrastructure.Interface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Infrastructure.Interface.Settings
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<object> GetDepartments(int page, int size, int statusId);
        Task<object> GetDepartmentById(int id);
        Task<ResponseModel> AddDepartment(DepartmentCreateVm model, int loginId);
        Task<ResponseModel> UpdateDepartment(DepartmentUpdateVm model, int loginId);
        Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId);
    }
}
