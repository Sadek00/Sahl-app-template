using AppTemplate.Domain.DBContexts;
using AppTemplate.Infrastructure.Interface.Common;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using static AppTemplate.Dto.Constants.AppConstants;

namespace AppTemplate.Infrastructure.Implementation.Common
{
    public class CommonRepository : ICommonRepository
    {
        private readonly AppDbContext _context;
        private readonly IDapperContext _dapper;

        public CommonRepository(AppDbContext context, IDapperContext dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task<object> GetDepartments()
        {
            try
            {
                var query = await _context.Departments.Where(x => x.StatusId == (byte)StatusId.Active)
                    .OrderBy(x => x.Name).Select(x => new
                    {
                        x.Id,
                        x.Name

                    }).ToListAsync();

                return query;
            }
            catch
            {
                throw;
            }
        }
        public async Task<object> GetDesignations()
        {
            try
            {
                var query = await _context.Designations.Where(x => x.StatusId == (byte)StatusId.Active)
                    .OrderBy(x => x.Name).Select(x => new
                    {
                        x.Id,
                        x.Name

                    }).ToListAsync();

                return query;
            }
            catch
            {
                throw;
            }
        }
        public async Task<object> GetUserTypes()
        {
            try
            {
                var query = await _context.UserTypes.Where(x => x.StatusId == (byte)StatusId.Active)
                    .OrderBy(x => x.Name).Select(x => new
                    {
                        x.Id,
                        x.Name

                    }).ToListAsync();

                return query;
            }
            catch
            {
                throw;
            }
        }

    }
}


