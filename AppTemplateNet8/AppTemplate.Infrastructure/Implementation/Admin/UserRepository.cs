using AppTemplate.Domain.DBContexts;
using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Dtos.Common;
using AppTemplate.Dto.Helpers;
using AppTemplate.Entities.DBManager;
using AppTemplate.Infrastructure.Implementation.Common;
using AppTemplate.Infrastructure.Interface.Admin;
using AppTemplate.Infrastructure.Interface.Common;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using static AppTemplate.Dto.Constants.AppConstants;
using static Dapper.SqlMapper;

namespace AppTemplate.Infrastructure.Implementation.Admin
{
    public class UserRepository(AppDbContext context, IDapperBaseRepository dapper, IHttpContextAccessor httpContextAccessor,
        IMapper mapper, IPasswordHasher passwordHasher) : BaseRepository<ApplicationUser>(context), IUserRepository
    {
        private readonly IDapperBaseRepository _dapper = dapper;
        private readonly AppDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<ResponseModelList> GetAll(int page, int size, int statusId)
        {
            try
            {
                var request = _httpContextAccessor.HttpContext.Request;

                var search = request.Query["search"].ToString();
                string sp = "public.sp_User_GetAll";
                var data = new PaginatedData<UserListVm>();

                DynamicParameters dynamicParameters = new();

                dynamicParameters.Add("total_rows", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var query = $"SELECT * FROM {sp}({page}, {size}, {statusId}, '{search}')";
                data.Data = (await _dapper.GetAllAsync<UserListVm>(query, dynamicParameters, CommandType.Text)).ToList();
                data.TotalRows = dynamicParameters.Get<int?>("total_rows");

                return SPManager.FinalPasignatedResultByNewKey(data, page, size);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseModel> AddUser(UserCreateVm model, int loginId)
        {
            #region Validation
            var userTypeExist = await _context.UserTypes.AnyAsync(x => x.Id == model.UserTypeId &&
                                       x.StatusId != (byte)StatusId.Delete);
            if (!userTypeExist)
                return Utilities.GetNoDataFoundMsg("User Type not found.");

            var userExists = await _context.Users.Where(x => (x.Username == model.Username
                                || x.Email == model.Email || x.PhoneNumber == model.PhoneNumber)
                                && x.StatusId != (byte)StatusId.Delete).ToListAsync();

            if (userExists.Count > 0)
            {
                if (userExists.Any(x => x.Email == model.Email))
                    return Utilities.GetValidationFailedMsg("Email already registered for another user.");
                if (userExists.Any(x => x.Username == model.Username))
                    return Utilities.GetValidationFailedMsg("Username already registered for another user.");
                if (userExists.Any(x => x.PhoneNumber == model.PhoneNumber))
                    return Utilities.GetValidationFailedMsg("Phone no already registered for another user.");
            }

            #endregion

            var guidGenerate = Guid.NewGuid().ToString();
            var hashedPassword = _passwordHasher.HashPassword(model.Password, guidGenerate);

            var newUser = _mapper.Map<ApplicationUser>(model);
            newUser.Password = hashedPassword;
            newUser.SecurityStamp = guidGenerate;
            newUser.CreatedAt = CommonMethods.GetBDCurrentTime();
            newUser.CreatedBy = loginId;
            newUser.StatusId = (int)StatusId.Active;

            _context.Users.Add(newUser);
            var dataSavedSuccess = await _context.SaveChangesAsync();

            if (dataSavedSuccess != 1)
            {
                return Utilities.GetValidationFailedMsg("User saving failed.");
            }
            else
            {
                return Utilities.GetSuccessMsg("Data saved successfully.", null);
            }
        }

        public async Task<ResponseModel> UpdateUser(UserUpdateVm model, int loginId, string token)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.Id
                                         && x.StatusId != (byte)StatusId.Delete);
            if (user is null)
                return Utilities.GetNoDataFoundMsg("User does not exist.");

            var userTypeExist = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.UserTypeId
                                               && x.StatusId != (byte)StatusId.Delete);
            if (userTypeExist is null)
                return Utilities.GetNoDataFoundMsg("User Type does not exist.");

            var userExists = await _context.Users.Where(x => (x.Username == model.Username
                                || x.Email == model.Email || x.PhoneNumber == model.PhoneNumber) && x.Id != model.Id
                                && x.StatusId != (byte)StatusId.Delete).ToListAsync();

            if (userExists.Count > 0)
            {
                if (userExists.Any(x => x.Email == model.Email))
                    return Utilities.GetValidationFailedMsg("Email already registered for another user.");
                if (userExists.Any(x => x.Username == model.Username))
                    return Utilities.GetValidationFailedMsg("Username already registered for another user.");
                if (userExists.Any(x => x.PhoneNumber == model.PhoneNumber))
                    return Utilities.GetValidationFailedMsg("Phone no already registered for another user.");
            }

            _mapper.Map(model, user);
            user.UpdatedAt = CommonMethods.GetBDCurrentTime();
            user.UpdatedBy = loginId;
            await _context.SaveChangesAsync();
            return Utilities.GetSuccessMsg("Data updated successfully.", null);

        }

        public async Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId, string token)
        {
            try
            {
                var entity = await _context.Users.FirstOrDefaultAsync(x => x.Id == id && x.StatusId != (byte)StatusId.Delete);
                if (entity is null)
                    return Utilities.GetErrorMsg(CommonMessages.NoDataFound);

                if (entity.StatusId == statusId)
                    return Utilities.GetErrorMsg(CommonMessages.Already + Enum.GetName(typeof(StatusId), statusId));

                entity.StatusId = statusId;
                entity.UpdatedBy = loginId;
                entity.UpdatedAt = CommonMethods.GetBDCurrentTime();
                await _context.SaveChangesAsync();
                return Utilities.GetSuccessMsg("Status changed successfully.", null);
            }
            catch (Exception e)
            {
                return Utilities.GetErrorMsg(e.Message);
            }
        }

        //private async Task<ResponseModel> CentralAuthenticationSave(UserCreateVm model, ApplicationUser newUser)
        //{
        //    var userCentralAuthVm = new UserCentralAuthVm
        //    {
        //        user_full_name = newUser.FullName,
        //        auth_mail = newUser.Email,
        //        auth_phone = newUser.PhoneNumber,
        //        service_key = _configuration["JWT:CentralAuthServiceKey"].ToString(),
        //        auth_username = newUser.Username,
        //        password = model.Password,
        //        is_mail_confirmed = false,
        //        is_phone_confirmed = false,
        //        enable_two_factor = false,
        //        user_mapping_id = newUser.Id.ToString(),
        //        is_admin = newUser.IsAdmin
        //    };
        //    var client = _httpClientFactory.CreateClient("CentralAuthenticationService");
        //    var json = JsonConvert.SerializeObject(userCentralAuthVm);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    Uri absoluteUri = new(client.BaseAddress.AbsoluteUri + "users/signup");
        //    var response = await client.PostAsync(absoluteUri, content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return Utilities.GetSuccessMsg("Saved Successful.", newUser);

        //    }
        //    else
        //    {
        //        string responseData = response.Content.ReadAsStringAsync().Result;
        //        var errorData = JsonConvert.DeserializeObject<CentralAuthResponseVm>(responseData);
        //        _context.Users.Remove(newUser);
        //        await _context.SaveChangesAsync();
        //        return Utilities.GetInternalServerErrorMsg(errorData.Message);
        //    }
        //}
    }
}
