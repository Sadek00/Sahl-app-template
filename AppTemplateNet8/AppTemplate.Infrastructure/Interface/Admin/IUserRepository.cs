using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Infrastructure.Interface.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Infrastructure.Interface.Admin
{
    public interface IUserRepository : IBaseRepository<ApplicationUser>
    {        
        Task<ResponseModelList> GetAll(int page, int size, int statusId);
        Task<ResponseModel> AddUser(UserCreateVm model, int loginId);
        Task<ResponseModel> UpdateUser(UserUpdateVm model, int loginId, string token);
        Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId, string token);
    }
}
