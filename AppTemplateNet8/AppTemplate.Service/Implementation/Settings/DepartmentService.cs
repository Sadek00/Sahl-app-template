using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Settings;
using AppTemplate.Infrastructure.Interface.Settings;
using AppTemplate.Service.Interface.Admin;
using AppTemplate.Service.Interface.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Service.Implementation.Settings
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<ResponseModel> Create(DepartmentCreateVm model, int loginId)
        {
            var res = await _departmentRepository.AddDepartment(model, loginId);
            return res;
        }

        public async Task<object> GetDepartmentById(int id)
        {
            return await _departmentRepository.GetDepartmentById(id);
        }

        public async Task<object> GetDepartments(int page, int size, int statusId)
        {
            return await _departmentRepository.GetDepartments(page, size, statusId);
        }

        public async Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId)
        {
            var res = await _departmentRepository.ModifyStatus(id, statusId, loginId);
            return res;
        }

        public async Task<ResponseModel> Update(DepartmentUpdateVm model, int loginId)
        {
            var res = await _departmentRepository.UpdateDepartment(model, loginId);
            return res;
        }
    }
}
