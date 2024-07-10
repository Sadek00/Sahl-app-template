using Microsoft.Extensions.DependencyInjection;
using AppTemplate.Service.Implementation.Admin;
using AppTemplate.Service.Implementation.Settings;
using AppTemplate.Service.Interface.Admin;
using AppTemplate.Service.Interface.Settings;
using AppTemplate.Service.Interface.Common;
using AppTemplate.Service.Implementation.Common;

namespace AppTemplate.Service.Helper
{
    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Admin
            services.AddScoped<IAuthenticateService, AuthenticateService>();            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserTypeService, UserTypeService>();            

            // Common
            services.AddScoped<ICommonService, CommonService>();

            //Settings
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService, DesignationService>();

            services.AddScoped<TokenService>();

            return services;
        }
    }
}
