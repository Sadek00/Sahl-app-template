using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using AppTemplate.Dto.Dtos.Admin;
using AppTemplate.Dto.Helpers;
using AppTemplate.Dto.Validators.Admin;
using AppTemplate.Service.Interface.Admin;
using static AppTemplate.Dto.Constants.AppConstants;

namespace AppTemplate.Api.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetUsers(int page, int size, int statusId)
        {
            try
            {
                var res = await _userService.GetAll(page, size, statusId);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }

        }
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserCreateVm model)
        {
            //return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg("User addition is currently unavailable. It will be available later."));
            try
            {
                FluentValidation.Results.ValidationResult validationResult = new UserCreateValidator().Validate(model);
                if (validationResult.IsValid)
                {
                    var res = await _userService.AddUser(model);

                    return StatusCode((int)res.StatusCode, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, Utilities.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateVm model)
        {
            //return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg("User addition is currently unavailable. It will be available later."));
            try
            {
                FluentValidation.Results.ValidationResult validationResult = new UserUpdateValidator().Validate(model);
                if (validationResult.IsValid)
                {
                    string token = GetAuthTokenFromRequest(HttpContext);
                    var res = await _userService.UpdateUser(model, token);
                    return StatusCode((int)res.StatusCode, res);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, Utilities.GetValidationFailedMsg(FluentValidationHelper.GetErrorMessage(validationResult.Errors)));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpGet("hidden")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public string? GetAuthTokenFromRequest(HttpContext context)
        {
            string authHeader = context.Request.Headers["Authorization"].ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                // Extract the token (excluding the "Bearer " prefix)
                string token = authHeader.Substring("Bearer ".Length).Trim();
                return token;
            }
            // If the Authorization header is missing or doesn't start with "Bearer ",
            // you can handle this case accordingly or return null.
            return null;
        }

        [HttpPost]
        [Route("ActiveUser/{id}")]
        public async Task<IActionResult> ActiveUser(int id)
        {
            try
            {
                string token = GetAuthTokenFromRequest(HttpContext);
                var res = await _userService.ModifyStatus(id, (int)StatusId.Active, token);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpPost]
        [Route("InActiveUser/{id}")]
        public async Task<IActionResult> InActiveUser(int id)
        {
            try
            {
                string token = GetAuthTokenFromRequest(HttpContext);
                var res = await _userService.ModifyStatus(id, (int)StatusId.InActive, token);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                string token = GetAuthTokenFromRequest(HttpContext);
                var res = await _userService.ModifyStatus(id, (int)StatusId.Delete, token);
                return StatusCode((int)res.StatusCode, res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }
    }
}
