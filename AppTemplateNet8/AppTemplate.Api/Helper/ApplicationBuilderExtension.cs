namespace AppTemplate.Api.Helper
{
    public static class ApplicationBuilderExtension
	{
		public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
		=> applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
	}
}
