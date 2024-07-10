using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Infrastructure.Interface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppTemplate.Infrastructure.Interface.Admin
{
    public interface IUserTypeRepository : IBaseRepository<UserType>
    {
        Task<object> GetUserTypes(int page, int size, int statusId);
        Task<object> GetUserTypeById(int id);
        Task<ResponseModel> AddUserType(UserTypeCreateVm model, int loginId);
        Task<ResponseModel> UpdateUserType(UserTypeUpdateVm model, int loginId);
        Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId);
    }
}