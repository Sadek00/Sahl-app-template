using Dapper;
using System.Data.Common;
using System.Data;
using Microsoft.Extensions.Configuration;
using AppTemplate.Infrastructure.Interface.Common;
//using Microsoft.Data.SqlClient;
using Npgsql;

namespace AppTemplate.Infrastructure.Repository.Common
{
    public class DapperBaseRepository : IDapperBaseRepository
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "DefaultConnection";
        private readonly IDbConnection _dbConnection;

        public DapperBaseRepository(IConfiguration config)
        {
            _config = config;
           // _dbConnection = new SqlConnection(_config.GetConnectionString(Connectionstring));
            _dbConnection = new NpgsqlConnection(_config.GetConnectionString(Connectionstring));
        }

        public void Dispose()
        {
            _dbConnection.Dispose();
        }

        public async Task<T> Get<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType);
        }
        public async Task<T> GetBySingleParamAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            return (await _dbConnection.QueryAsync<T>(sp, parms, commandType: commandType)).FirstOrDefault();
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, CommandType commandType = CommandType.StoredProcedure)
        {
            return await _dbConnection.QueryAsync<T>(sp, commandType: commandType);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            return await _dbConnection.QueryAsync<T>(sp, parms, commandType: commandType);
        }
        public async Task<IEnumerable<T>> GetAllAsyncPG<T>(string functionName, DynamicParameters parms)
        {
            var query = "SELECT * FROM " + functionName;
            var paramsNames = "";
            foreach (var name in parms.ParameterNames)
            {
                paramsNames += name;
            }
            query += string.Join("(", paramsNames, ")");
            return await _dbConnection.QueryAsync<T>(query, parms, commandType: CommandType.Text);
        }
        public async Task<IEnumerable<T>> GetAllByPostAsync<T>(string sp)
        {
            using IDbConnection db = new NpgsqlConnection(_config.GetConnectionString(Connectionstring));
            return await db.QueryAsync<T>(sp);
        }
        public async Task<(T, List<TT>)> GetAsyncMultiple<T, TT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            var data = (await _dbConnection.QueryMultipleAsync(sp, parms, commandType: commandType));
            var res1 = data.Read<T>().FirstOrDefault();
            var res2 = data.Read<TT>().ToList();
            return (res1, res2);
        }
        public async Task<(T, List<TT>, List<TTT>)> GetAsyncMultiple<T, TT, TTT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            var data = (await _dbConnection.QueryMultipleAsync(sp, parms, commandType: commandType));
            var res1 = data.Read<T>().FirstOrDefault();
            var res2 = data.Read<TT>().ToList();
            var res3 = data.Read<TTT>().ToList();
            return (res1, res2, res3);
        }
        public async Task<(T, List<TT>, List<TTT>, List<TTTT>)> GetAsyncMultiple<T, TT, TTT, TTTT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            var data = (await _dbConnection.QueryMultipleAsync(sp, parms, commandType: commandType));
            var res1 = data.Read<T>().FirstOrDefault();
            var res2 = data.Read<TT>().ToList();
            var res3 = data.Read<TTT>().ToList();
            var res4 = data.Read<TTTT>().ToList();
            return (res1, res2, res3, res4);
        }
        public async Task<IEnumerable<T>> GetDropdownList<T>(string sp)
        {
            return await _dbConnection.QueryAsync<T>(sp, commandType: CommandType.StoredProcedure);
        }

        public async Task<object> Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            var data = (await _dbConnection.QueryAsync<string>(sp, parms, commandType: commandType)).FirstOrDefault();
            return data;
        }
        public async Task<int> ExecuteAsync<T>(string sp, T entity)
        {
            try
            {
                var result = await _dbConnection.ExecuteAsync(sp, entity);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /*  CRUD operation not needed for Dapper

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();

                using var tran = _dbConnection.BeginTransaction();
                try
                {
                    result = _dbConnection.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
            return result;
        }
        public T Delete<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();

                using var tran = _dbConnection.BeginTransaction();
                try
                {
                    result = _dbConnection.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }

            return result;
        }
        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            try
            {
                if (_dbConnection.State == ConnectionState.Closed)
                    _dbConnection.Open();

                using var tran = _dbConnection.BeginTransaction();
                try
                {
                    result = _dbConnection.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_dbConnection.State == ConnectionState.Open)
                    _dbConnection.Close();
            }
            return result;
        }
        */

        public async Task<IEnumerable<dynamic>> GetReportData(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            List<dynamic> result = (await _dbConnection.QueryAsync(sp, parms, commandType: commandType)).ToList();
            return result;
        }
        public async Task<List<dynamic>> GetJsonData(string sp)
        {
            var result = (await _dbConnection.QueryAsync(sp));           
            return (List<dynamic>)result;
        }
    }
}
