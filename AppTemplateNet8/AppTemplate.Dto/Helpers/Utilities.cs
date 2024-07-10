using AppTemplate.Domain.Utilities;
using System.Net;
using static AppTemplate.Dto.Constants.AppConstants;

namespace AppTemplate.Dto.Helpers
{
    public static class Utilities
    {
        public static ResponseModel GetSuccessMsg(string message, object? data = null)
        {
            return new ResponseModel
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Status = ResultStatus.Success.ToString(),
                Message = message,
                Data = data
            };
        }
        public static ResponseModel GetAlreadyExistMsg(string message)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.Conflict,
                Status = ResultStatus.Canceled.ToString(),
                Message = message,
                Data = null
            };
        }
        public static ResponseModel GetErrorMsg(string message)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Status = ResultStatus.Canceled.ToString(),
                Message = message
            };
        }
        public static ResponseModel GetInternalServerErrorMsg(Exception ex, string? customMessage = null)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Status = ResultStatus.Error.ToString(),
                Message = string.IsNullOrWhiteSpace(customMessage) ? (ex.Message + (ex.InnerException != null ? " --> InnerException: " + ex.InnerException.Message : "")) : customMessage,
                Data = null
            };
        }
        public static ResponseModel GetInternalServerErrorMsg(string message)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Status = ResultStatus.Error.ToString(),
                Message = message,
                Data = null
            };
        }
        public static ResponseModel GetNoDataFoundMsg(string message)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.NotFound,
                Status = ResultStatus.Error.ToString(),
                Message = message,
                Data = null
            };
        }
        public static ResponseModel GetUnauthenticatedMsg()
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.Unauthorized,
                Status = ResultStatus.Canceled.ToString(),
                Message = CommonMessages.Unauthenticated,
                Data = null
            };
        }
        public static ResponseModel GetUnauthorizedMsg()
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.Unauthorized,
                Status = ResultStatus.Canceled.ToString(),
                Message = CommonMessages.Unauthorized,
                Data = null
            };
        }
        public static ResponseModel GetValidationFailedMsg(string message)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Status = ResultStatus.Canceled.ToString(),
                Message = CommonMessages.ValidationFailed,
                Data = null
            };
        }
        public static ResponseModel GetValidationFailedMsg(List<string> messageList)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Status = ResultStatus.Canceled.ToString(),
                Message = CommonMessages.ValidationFailed,
                Data = messageList
            };
        }
        public static ResponseModel GetImageSaveFailedMsg(string message)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.UnprocessableEntity,
                Status = ResultStatus.Error.ToString(),
                Message = CommonMessages.ImageSaveFailed,
                Data = null
            };
        }
    }
}
