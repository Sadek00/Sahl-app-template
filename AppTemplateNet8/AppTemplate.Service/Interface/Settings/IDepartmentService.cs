using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Service.Interface.Settings
{
    public interface IDepartmentService
    {
        Task<object> GetDepartments(int page, int size, int statusId);
        Task<object> GetDepartmentById(int id);
        Task<ResponseModel> Create(DepartmentCreateVm model, int loginId);
        Task<ResponseModel> Update(DepartmentUpdateVm model, int loginId);
        Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId);
    }
}
