using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Settings;
using AppTemplate.Dto.Helpers;
using AppTemplate.Dto.Validators.Settings;
using AppTemplate.Service.Interface.Settings;
using static AppTemplate.Dto.Constants.AppConstants;

namespace AppTemplate.Api.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentController(IDepartmentService departmentService, IHttpContextAccessor httpContextAccessor)
        {
            _departmentService = departmentService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments(int page, int size, int statusId)
        {
            try
            {
                var res = await _departmentService.GetDepartments(page, size, statusId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpGet]
        [Route("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var res = await _departmentService.GetDepartmentById(id);

                if (res == null)
                    return StatusCode(StatusCodes.Status404NotFound);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentCreateVm model)
        {
            try
            {
                ResponseModel res = null;
                var validationResult = new DepartmentCreateValidator().Validate(model);
                if (validationResult.IsValid)
                {
                    int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    res = await _departmentService.Create(model, userId);
                }
                else
                {
                    res = Utilities.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
                }
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpPost]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentUpdateVm model)
        {
            try
            {
                ResponseModel res = null;
                var validationResult = new DepartmentUpdateValidator().Validate(model);
                if (validationResult.IsValid)
                {
                    int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    res = await _departmentService.Update(model, userId);
                }
                else
                {
                    res = Utilities.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors));
                }
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpPost]
        [Route("ActiveDepartment/{id}")]
        public async Task<IActionResult> ActiveDepartment(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _departmentService.ModifyStatus(id, (int)StatusId.Active, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpPost]
        [Route("InActiveDepartment/{id}")]
        public async Task<IActionResult> InActiveDepartment(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _departmentService.ModifyStatus(id, (int)StatusId.InActive, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpDelete]
        [Route("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _departmentService.ModifyStatus(id, (int)StatusId.Delete, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }
    }
}
