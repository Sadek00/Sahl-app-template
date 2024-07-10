using AppTemplate.Dto.Helpers;
using System.Net;
using System.Text.Json;

namespace AppTemplate.Api.Helper
{
    public class GlobalErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

		public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
				await HandleExceptionAsync(context, exception);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";
			var response = context.Response;

			var errorResponse = new ErrorResponse
			{
				Success = false,
				Data = exception.Data.ToString(),
				Message = exception.InnerException != null ? exception.InnerException.Message : exception.Message
			};

			switch (exception.GetType().Name)
			{
				case nameof(BadRequestException):
					response.StatusCode = (int)HttpStatusCode.BadRequest;
					break;
				case nameof(NotFoundException):
					response.StatusCode = (int)HttpStatusCode.NotFound;
					break;
				case nameof(NotImplementedException):
					response.StatusCode = (int)HttpStatusCode.NotImplemented;
					break;
				case nameof(UnauthorizedAccessException):
					response.StatusCode = (int)HttpStatusCode.Unauthorized;
					break;
				case nameof(KeyNotFoundException):
					response.StatusCode = (int)HttpStatusCode.NotFound;
					break;
				default:
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					break;
			}
			return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
		}
	}
}
