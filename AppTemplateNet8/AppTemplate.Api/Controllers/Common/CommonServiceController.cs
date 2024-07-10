using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AppTemplate.Dto.Helpers;
using System.ComponentModel.DataAnnotations;
using AppTemplate.Service.Interface.Common;


namespace AppTemplate.Api.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonServiceController : ControllerBase
    {
        private readonly ICommonService _iService;

        public CommonServiceController(ICommonService iUserService)
        {
            _iService = iUserService;
        }

        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var res = await _iService.GetDepartments();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }
        [HttpGet]
        [Route("GetDesignations")]
        public async Task<IActionResult> GetDesignations()
        {
            try
            {
                var res = await _iService.GetDesignations();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }
        [HttpGet]
        [Route("GetUserTypes")]
        public async Task<IActionResult> GetUserTypes()
        {
            try
            {
                var res = await _iService.GetUserTypes();

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Utilities.GetInternalServerErrorMsg(ex));
            }
        }
    }
}
