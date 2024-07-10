using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Service.Interface.Admin
{
    public interface IUserService
    {
        Task<UserDto> GetByUsernameAsync(string username);
        Task<UserDto> GetByCredentialAsync(string username,string password);

        Task<ResponseModelList> GetAll(int page, int size, int statusId);
        //Task<object> GetUserById(int id);
        ApplicationUser GetCurrentUser();
        Task<ResponseModel> AddUser(UserCreateVm model);
        Task<ResponseModel> UpdateUser(UserUpdateVm model, string token);
        Task<ResponseModel> ModifyStatus(int id, int statusId, string token);        
    }
}
