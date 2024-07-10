
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Settings;

namespace AppTemplate.Service.Interface.Settings
{
    public interface IDesignationService
    {
        Task<object> GetDesignations(int page, int size, int statusId);
        Task<object> GetDesignationById(int id);
        Task<ResponseModel> Create(DesignationCreateVm model, int loginId);
        Task<ResponseModel> Update(DesignationUpdateVm model, int loginId);
        Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId);
    }
}
