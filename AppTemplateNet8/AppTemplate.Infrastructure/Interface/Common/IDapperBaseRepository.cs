using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AppTemplate.Infrastructure.Interface.Common
{
	public interface IDapperBaseRepository : IDisposable
	{
        Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		Task<T> GetBySingleParamAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		Task<IEnumerable<T>> GetAllAsync<T>(string sp, CommandType commandType = CommandType.StoredProcedure);
		Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetAllByPostAsync<T>(string sp);
        Task<(T, List<TT>)> GetAsyncMultiple<T, TT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<(T, List<TT>, List<TTT>)> GetAsyncMultiple<T, TT, TTT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<(T, List<TT>, List<TTT>, List<TTTT>)> GetAsyncMultiple<T, TT, TTT, TTTT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetDropdownList<T>(string sp);
		
		Task<object> Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		Task<int> ExecuteAsync<T>(string sp, T entity);

        /*  CRUD operation not needed for Dapper

		T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
		T Delete<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);
		*/

        Task<IEnumerable<dynamic>> GetReportData(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<List<dynamic>> GetJsonData(string sp);
    }
}
