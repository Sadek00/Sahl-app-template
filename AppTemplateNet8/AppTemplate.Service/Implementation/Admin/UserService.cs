using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Helpers;
using AppTemplate.Infrastructure.Interface.Admin;
using AppTemplate.Service.Helper;
using AppTemplate.Service.Interface.Admin;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppTemplate.Dto.Constants.AppConstants;

namespace AppTemplate.Service.Implementation.Admin
{
    public class UserService(IUserRepository userRepository, TokenService tokenService, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly TokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = (await _userRepository.GetAsync(x => x.Username == username)).FirstOrDefault();
            return _mapper.Map<UserDto>(user);
        }
        public async Task<UserDto> GetByCredentialAsync(string username, string password)
        {
            var user = (await _userRepository.GetAsync(x => x.Username == username && x.Password == password)).FirstOrDefault();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<ResponseModelList> GetAll(int page, int size, int statusId)
        {
            return await _userRepository.GetAll(page, size, statusId);
        }
        public ApplicationUser GetCurrentUser()
        {
            //var user1 = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).Result;

            ApplicationUser user = new ApplicationUser { Id = 1, FullName = "Admin", Username = "admin", Password = "Admin@123", UserTypeId = 1 };
            return user;
        }
        public async Task<ResponseModel> AddUser(UserCreateVm model)
        {
            var user = _mapper.Map<ApplicationUser>(model);
            user.CreatedAt = CommonMethods.GetBDCurrentTime();
            user.CreatedBy = Convert.ToInt32(_tokenService.GetUserMappingIdFromToken());
            user.StatusId = (int)StatusId.Active;

            var loginId = GetCurrentUser().Id;
            return await _userRepository.AddUser(model,loginId);
        }
        public async Task<ResponseModel> UpdateUser(UserUpdateVm model, string token)
        {
            var loginId = GetCurrentUser().Id;
            return await _userRepository.UpdateUser(model, loginId, token);
        }
        public async Task<ResponseModel> ModifyStatus(int id, int statusId, string token)
        {
            var loginId = GetCurrentUser().Id;
            var res = await _userRepository.ModifyStatus(id, statusId, loginId, token);
            return res;
        }                
    }
}
