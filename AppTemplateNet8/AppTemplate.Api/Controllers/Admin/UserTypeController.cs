using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppTemplate.Dto.Helpers;
using static AppTemplate.Dto.Constants.AppConstants;
using AppTemplate.Service.Interface.Admin;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Validators.Admin;

namespace AppTemplate.Api.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserTypeService _userTypeService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserTypeController(IUserTypeService userTypeService, IHttpContextAccessor httpContextAccessor)
        {
            _userTypeService = userTypeService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetUserTypes")]
        public async Task<IActionResult> GetUserTypes(int page, int size, int statusId)
        {
            try
            {
                var res = await _userTypeService.GetUserTypes(page, size, statusId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpGet]
        [Route("GetUserTypeById/{id}")]
        public async Task<IActionResult> GetUserTypeById(int id)
        {
            try
            {
                var res = await _userTypeService.GetUserTypeById(id);

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
        [Route("AddUserType")]
        public async Task<IActionResult> AddUserType([FromBody] UserTypeCreateVm model)
        {
            try
            {
                ResponseModel res = null;
                var validationResult = new UserTypeCreateValidator().Validate(model);
                if (validationResult.IsValid)
                {
                    int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    res = await _userTypeService.Create(model, userId);
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
        [Route("UpdateUserType")]
        public async Task<IActionResult> UpdateUserType([FromBody] UserTypeUpdateVm model)
        {
            try
            {
                ResponseModel res = null;
                var validationResult = new UserTypeUpdateValidator().Validate(model);
                if (validationResult.IsValid)
                {
                    int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    res = await _userTypeService.Update(model, userId);
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
        [Route("ActiveUserType/{id}")]
        public async Task<IActionResult> ActiveUserType(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _userTypeService.ModifyStatus(id, (int)StatusId.Active, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpPost]
        [Route("InActiveUserType/{id}")]
        public async Task<IActionResult> InActiveUserType(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _userTypeService.ModifyStatus(id, (int)StatusId.InActive, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpDelete]
        [Route("DeleteUserType/{id}")]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _userTypeService.ModifyStatus(id, (int)StatusId.Delete, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }
    }
}