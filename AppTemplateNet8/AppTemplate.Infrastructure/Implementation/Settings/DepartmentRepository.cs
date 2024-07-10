using AppTemplate.Domain.DBContexts;
using AppTemplate.Domain.Entities.Settings;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Settings;
using AppTemplate.Dto.Helpers;
using AppTemplate.Entities.DBManager;
using AppTemplate.Infrastructure.Implementation.Common;
using AppTemplate.Infrastructure.Interface.Settings;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AppTemplate.Dto.Constants.AppConstants;

namespace AppTemplate.Infrastructure.Implementation.Settings
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly IDapperContext _dapper;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _context;

        public DepartmentRepository(
            AppDbContext context,
            IDapperContext dapper,
            IMapper mapper,IHttpContextAccessor httpContextAccessor) : base(context)
        {
            _dapper = dapper;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<object> GetDepartments(int page, int size, int statusId)
        {
            try
            {
                var request = _httpContextAccessor.HttpContext.Request;

                var name = request.Query["name"].ToString();
                string sp = "public.sp_Department_GetAll";
                var data = new PaginatedData<DesignationListVm>();

                DynamicParameters dynamicParameters = new DynamicParameters();

                dynamicParameters.Add("total_rows", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var query = $"SELECT * FROM {sp}({page}, {size}, {statusId})";
                data.Data = (await _dapper.GetAllAsync<DesignationListVm>(query, dynamicParameters, CommandType.Text)).ToList();
                data.TotalRows = dynamicParameters.Get<int?>("total_rows");

                return SPManager.FinalPasignatedResultByNewKey(data, page, size);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<object> GetDepartmentById(int id)
        {
            try
            {
                DynamicParameters dynamicParameters = new DynamicParameters();

                var query = $"SELECT * FROM public.sp_Department_GetbyId({id})";
                var result = await _dapper.GetAsync<DepartmentListVm>(query, dynamicParameters, CommandType.Text);
                return result;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<ResponseModel> AddDepartment(DepartmentCreateVm model, int loginId)
        {
            try
            {
                var deparmentExist = await _context.Departments.AnyAsync(x => x.Name == model.Name
                                       && x.StatusId != (byte)StatusId.Delete);
                if (deparmentExist)
                    return Utilities.GetAlreadyExistMsg("Designation already exist.");

                var entity = _mapper.Map<Department>(model);

                entity.StatusId = (byte)StatusId.Active;
                entity.CreatedAt = CommonMethods.GetBDCurrentTime();
                entity.CreatedBy = loginId;

                _context.Departments.Add(entity);
                await _context.SaveChangesAsync();

                return Utilities.GetSuccessMsg(CommonMessages.SavedSuccessfully, entity);
            }
            catch (Exception ex)
            {
                return Utilities.GetErrorMsg(ex.Message);
            }
        }
        public async Task<ResponseModel> UpdateDepartment(DepartmentUpdateVm model, int loginId)
        {
            try
            {
                var deparmentExist = await _context.Departments.AnyAsync(x => x.Id != model.Id && x.Name == model.Name &&
                                       x.StatusId != (byte)StatusId.Delete).ConfigureAwait(true);
                if (deparmentExist)
                    return Utilities.GetAlreadyExistMsg("Department already exist.");

                var entity = await _context.Departments.FirstOrDefaultAsync(x => x.Id == model.Id && x.StatusId == (byte)StatusId.Active);
                if (entity is null)
                    return Utilities.GetNoDataFoundMsg("Deparment with the porvided Id does not exist.");

                _mapper.Map(model, entity);
                entity.UpdatedBy = loginId;
                entity.UpdatedAt = CommonMethods.GetBDCurrentTime();

                _context.Departments.Update(entity);
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
                var entity = await _context.Departments.FirstOrDefaultAsync(x => x.Id == id && x.StatusId != (byte)StatusId.Delete).ConfigureAwait(true);
                if (entity is null)
                    return Utilities.GetErrorMsg(CommonMessages.NoDataFound);

                if (entity.StatusId == statusId)
                    return Utilities.GetErrorMsg(CommonMessages.Already + Enum.GetName(typeof(StatusId), statusId));

                entity.StatusId = statusId;
                entity.UpdatedBy = loginId;
                entity.UpdatedAt = CommonMethods.GetBDCurrentTime();

                _context.Departments.Update(entity);
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
