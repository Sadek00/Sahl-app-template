using AppTemplate.Domain.AppConstants;
using AppTemplate.Infrastructure.Implementation.Admin;
using AppTemplate.Infrastructure.Implementation.Common;
using AppTemplate.Infrastructure.Implementation.Settings;
using AppTemplate.Infrastructure.Interface.Admin;
using AppTemplate.Infrastructure.Interface.Common;
using AppTemplate.Infrastructure.Interface.Settings;
using AppTemplate.Infrastructure.Repository.Common;
using Microsoft.Extensions.DependencyInjection;

namespace AppTemplate.Infrastructure.Helper
{
    public static class InfrastructuresRegistration
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            //            
            services.AddSingleton<IJwtTokenUtility>(new JwtTokenUtility(AppConstants.JwtSecretKey));

            // Admin            
            services.AddScoped<IUserRepository, UserRepository>();            
            services.AddScoped<IUserTypeRepository, UserTypeRepository>();

            // Common
            services.AddScoped<ICommonRepository, CommonRepository>();
            services.AddScoped<IDapperBaseRepository, DapperBaseRepository>();

            //Settings
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            
            return services;
        }
    }
}
