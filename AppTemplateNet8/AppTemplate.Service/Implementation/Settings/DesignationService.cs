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
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository _designationRepository;

        public DesignationService(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        public async Task<ResponseModel> Create(DesignationCreateVm model, int loginId)
        {
            var res = await _designationRepository.AddDesignation(model, loginId);
            return res;
        }

        public async Task<object> GetDesignationById(int id)
        {
            return await _designationRepository.GetDesignationById(id);
        }

        public async Task<object> GetDesignations(int page, int size, int statusId)
        {
            return await _designationRepository.GetDesignations(page, size, statusId);
        }

        public async Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId)
        {
            var res = await _designationRepository.ModifyStatus(id, statusId, loginId);
            return res;
        }

        public async Task<ResponseModel> Update(DesignationUpdateVm model, int loginId)
        {
            var res = await _designationRepository.UpdateDesignation(model, loginId);
            return res;
        }
    }
}
