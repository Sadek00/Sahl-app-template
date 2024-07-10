
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;

namespace AppTemplate.Service.Interface.Admin
{
    public interface IUserTypeService
    {
        Task<object> GetUserTypes(int page, int size, int statusId);
        Task<object> GetUserTypeById(int id);
        Task<ResponseModel> Create(UserTypeCreateVm model, int loginId);
        Task<ResponseModel> Update(UserTypeUpdateVm model, int loginId);
        Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId);
    }
}