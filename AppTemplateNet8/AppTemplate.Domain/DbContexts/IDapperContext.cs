using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace AppTemplate.Domain.DBContexts
{
    public interface IDapperContext : IDisposable
    {
        Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<(T, List<TT>)> GetAsyncMultiple<T, TT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<(T, List<TT>, List<TTT>)> GetAsyncTriple<T, TT, TTT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<(T, List<TT>, List<TTT>, List<TTTT>)> GetAsyncMultiples<T, TT, TTT, TTTT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetAllAsync<T>(string sp, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<T> GetBySingleParamAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<IEnumerable<T>> GetDropdownList<T>(string sp);
        Task<List<dynamic>> GetJsonData(string sp);
        
        T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        T Delete<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text);

        Task<object> Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        Task<int> ExecuteAsync<T>(string sp, T entity);
    }
}
