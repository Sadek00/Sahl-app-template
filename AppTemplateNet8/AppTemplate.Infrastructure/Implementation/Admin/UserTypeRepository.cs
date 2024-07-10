using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AppTemplate.Domain.DBContexts;
using AppTemplate.Domain.Entities.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Helpers;
using AppTemplate.Entities.DBManager;
using AppTemplate.Infrastructure.Implementation.Common;
using AppTemplate.Infrastructure.Interface.Admin;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static AppTemplate.Dto.Constants.AppConstants;

namespace AppTemplate.Infrastructure.Implementation.Admin
{
    public class UserTypeRepository : BaseRepository<UserType>, IUserTypeRepository
    {
        private readonly IDapperContext _dapper;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public UserTypeRepository(AppDbContext context, IDapperContext dapper, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _dapper = dapper;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<object> GetUserTypes(int page, int size, int statusId)
        {
            try
            {
                var request = _httpContextAccessor.HttpContext.Request;

                var search = request.Query["search"].ToString();
                string sp = "public.sp_UserType_GetAll";
                var data = new PaginatedData<UserTypeListVm>();

                DynamicParameters dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("total_rows", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var query = $"SELECT * FROM {sp}({page}, {size}, {statusId}, '{search}')";
                data.Data = (await _dapper.GetAllAsync<UserTypeListVm>(query, dynamicParameters, CommandType.Text)).ToList();
                data.TotalRows = dynamicParameters.Get<int?>("total_rows");

                return SPManager.FinalPasignatedResultByNewKey(data, page, size);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<object> GetUserTypeById(int id)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();

                var query = $"SELECT * FROM public.sp_UserType_GetbyId({id})";
                var result = await _dapper.GetAsync<UserTypeListVm>(query, dynamicParameters, CommandType.Text);
                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<ResponseModel> AddUserType(UserTypeCreateVm model, int loginId)
        {
            try
            {
                var userTypeExistExist = await _context.UserTypes.AnyAsync(x => x.Name == model.Name
                                       && x.StatusId != (byte)StatusId.Delete);
                if (userTypeExistExist)
                    return Utilities.GetAlreadyExistMsg("User type already exist.");

                var entity = _mapper.Map<UserType>(model);

                entity.StatusId = (byte)StatusId.Active;
                entity.CreatedAt = CommonMethods.GetBDCurrentTime();
                entity.CreatedBy = loginId;

                _context.UserTypes.Add(entity);
                await _context.SaveChangesAsync();

                return Utilities.GetSuccessMsg(CommonMessages.SavedSuccessfully, entity);
            }
            catch (Exception ex)
            {
                return Utilities.GetErrorMsg(ex.Message);
            }
        }
        public async Task<ResponseModel> UpdateUserType(UserTypeUpdateVm model, int loginId)
        {
            try
            {
                var userTypeExist = await _context.UserTypes.AnyAsync(x => x.Id != model.Id && x.Name == model.Name &&
                                       x.StatusId != (byte)StatusId.Delete);
                if (userTypeExist)
                    return Utilities.GetAlreadyExistMsg("User type already exist.");

                var entity = await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == model.Id && x.StatusId == (byte)StatusId.Active);
                if (entity is null) return Utilities.GetNoDataFoundMsg("User Type with the Id is not found.");

                _mapper.Map(model, entity);
                entity.UpdatedBy = loginId;
                entity.UpdatedAt = CommonMethods.GetBDCurrentTime();

                _context.UserTypes.Update(entity);
                await _context.SaveChangesAsync();

                return Utilities.GetSuccessMsg(CommonMessages.UpdatedSuccessfully, entity);
            }
            catch (Exception e)
            {
                return Utilities.GetErrorMsg(e.Message);
            }
        }
        public async Task<ResponseModel> ModifyStatus(int id, int statusId, int loginId)
        {
            try
            {
                var entity = await _context.UserTypes.FirstOrDefaultAsync(x => x.Id == id && x.StatusId != (byte)StatusId.Delete).ConfigureAwait(true);
                if (entity is null)
                    return Utilities.GetErrorMsg(CommonMessages.NoDataFound);

                if (entity.StatusId == statusId)
                    return Utilities.GetErrorMsg(CommonMessages.Already + Enum.GetName(typeof(StatusId), statusId));

                entity.StatusId = statusId;
                entity.UpdatedBy = loginId;
                entity.UpdatedAt = CommonMethods.GetBDCurrentTime();

                _context.UserTypes.Update(entity);
                await _context.SaveChangesAsync();

                return Utilities.GetSuccessMsg(statusId == (byte)StatusId.Delete ?
                    CommonMessages.DeletedSuccessfully : statusId == (byte)StatusId.Active ? CommonMessages.ActiveSuccessfully : CommonMessages.InactiveSuccessfully, entity);
            }
            catch (Exception e)
            {
                return Utilities.GetErrorMsg(e.Message);
            }
        }
    }
}