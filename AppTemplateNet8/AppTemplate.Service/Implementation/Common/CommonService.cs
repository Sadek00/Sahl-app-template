using AppTemplate.Infrastructure.Interface.Common;
using AppTemplate.Service.Interface.Common;
using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;

namespace AppTemplate.Service.Implementation.Common
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommonService(ICommonRepository commonRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commonRepository = commonRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object> GetDepartments()
        {
            return await _commonRepository.GetDepartments();
        }
        public async Task<object> GetDesignations()
        {
            return await _commonRepository.GetDesignations();
        }
        public async Task<object> GetUserTypes()
        {
            return await _commonRepository.GetUserTypes();
        }
    }
}
