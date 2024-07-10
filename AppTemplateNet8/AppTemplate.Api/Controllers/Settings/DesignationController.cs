using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppTemplate.Domain.Utilities;
using AppTemplate.Dto.Dtos.Settings;
using AppTemplate.Dto.Helpers;
using AppTemplate.Dto.Validators.Settings;
using AppTemplate.Service.Implementation.Settings;
using AppTemplate.Service.Interface.Settings;
using static AppTemplate.Dto.Constants.AppConstants;

namespace AppTemplate.Api.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DesignationController(IDesignationService designationService, IHttpContextAccessor httpContextAccessor)
        {
            _designationService = designationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("GetDesignations")]
        public async Task<IActionResult> GetDesignations(int page, int size, int statusId)
        {
            try
            {
                var res = await _designationService.GetDesignations(page, size, statusId);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpGet]
        [Route("GetDesignationById/{id}")]
        public async Task<IActionResult> GetDesignationById(int id)
        {
            try
            {
                var res = await _designationService.GetDesignationById(id);

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
        [Route("AddDesignation")]
        public async Task<IActionResult> AddDesignation([FromBody] DesignationCreateVm model)
        {
            try
            {
                ResponseModel res = null;
                var validationResult = new DesignationCreateValidator().Validate(model);
                if (validationResult.IsValid)
                {
                    int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    res = await _designationService.Create(model, userId);
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
        [Route("UpdateDesignation")]
        public async Task<IActionResult> UpdateDesignation([FromBody] DesignationUpdateVm model)
        {
            try
            {
                ResponseModel res = null;
                var validationResult = new DesignationUpdateValidator().Validate(model);
                if (validationResult.IsValid)
                {
                    int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                    res = await _designationService.Update(model, userId);
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
        [Route("ActiveDesignation/{id}")]
        public async Task<IActionResult> ActiveDesignation(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _designationService.ModifyStatus(id, (int)StatusId.Active, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpPost]
        [Route("InActiveDesignation/{id}")]
        public async Task<IActionResult> InActiveDesignation(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _designationService.ModifyStatus(id, (int)StatusId.InActive, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpDelete]
        [Route("DeleteDesignation/{id}")]
        public async Task<IActionResult> DeleteDesignation(int id)
        {
            try
            {
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var res = await _designationService.ModifyStatus(id, (int)StatusId.Delete, userId);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }
    }
}
