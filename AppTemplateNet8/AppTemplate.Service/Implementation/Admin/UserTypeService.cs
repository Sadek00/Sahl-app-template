using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Infrastructure.Interface.Admin;
using AppTemplate.Service.Interface.Admin;

namespace AppTemplate.Service.Implementation.Admin
{
    public class UserTypeService : IUserTypeService
    {
        private readonly IUserTypeRepository _userTypeRepository;

        public UserTypeService(IUserTypeRepository userTypeRepository)
        {
            _userTypeRepository = userTypeRepository;
        }

        public async Task<ResponseModel> Create(UserTypeCreateVm model, int loginId)
        {
            var res = await _userTypeRepository.AddUserType(model, loginId);
            return res;
        }

        public async Task<object> GetUserTypeById(int id)
        {
            return await _userTypeRepository.GetUserTypeById(id);
        }

        public async Task<object> GetUserTypes(int page, int size, int statusId)
        {
            return await _userTypeRepository.GetUserTypes(page, size, statusId);
        }

        public async Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId)
        {
            var res = await _userTypeRepository.ModifyStatus(id, statusId, loginId);
            return res;
        }

        public async Task<ResponseModel> Update(UserTypeUpdateVm model, int loginId)
        {
            var res = await _userTypeRepository.UpdateUserType(model, loginId);
            return res;
        }
    }
}