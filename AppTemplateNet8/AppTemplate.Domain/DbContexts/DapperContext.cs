using Dapper;
using Microsoft.Extensions.Configuration;
using AppTemplate.Domain.DBContexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace AppTemplate.Domain.DBContexts
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _config;
        private readonly IDbConnection _dbConnection;
        private readonly string _connString = "DefaultConnection";
        public DapperContext(IConfiguration config)
        {
            _config = config;
            //_dbConnection = new SqlConnection(_config.GetConnectionString(_connString));
            _dbConnection = new NpgsqlConnection(_config.GetConnectionString(_connString));
        }
        public void Dispose()
        {
            _dbConnection.Dispose();
        }

        public async Task<T> GetAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(sp, parms, commandType: commandType);
        }
        public async Task<(T, List<TT>)> GetAsyncMultiple<T, TT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = _dbConnection;
            var data = (await db.QueryMultipleAsync(sp, parms, commandType: commandType));
            var res1 = data.Read<T>().FirstOrDefault();
            var res2 = data.Read<TT>().ToList();
            return (res1, res2);
        }        
        public async Task<(T, List<TT>, List<TTT>)> GetAsyncTriple<T, TT, TTT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = _dbConnection;
            var data = (await db.QueryMultipleAsync(sp, parms, commandType: commandType));
            var res1 = data.Read<T>().FirstOrDefault();
            var res2 = data.Read<TT>().ToList();
            var res3 = data.Read<TTT>().ToList();
            return (res1, res2, res3);
        }
        public async Task<(T, List<TT>, List<TTT>, List<TTTT>)> GetAsyncMultiples<T, TT, TTT, TTTT>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = _dbConnection;
            var data = (await db.QueryMultipleAsync(sp, parms, commandType: commandType));
            var res1 = data.Read<T>().FirstOrDefault();
            var res2 = data.Read<TT>().ToList();
            var res3 = data.Read<TTT>().ToList();
            var res4 = data.Read<TTTT>().ToList();
            return (res1, res2, res3, res4);
        }        
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = _dbConnection;
            return await db.QueryAsync<T>(sp, commandType: commandType);
        }
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = _dbConnection;
            return await db.QueryAsync<T>(sp, parms, commandType: commandType);
        }
        public async Task<T> GetBySingleParamAsync<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = _dbConnection;
            return (await db.QueryAsync<T>(sp, parms, commandType: commandType)).FirstOrDefault();
        }
        public async Task<IEnumerable<T>> GetDropdownList<T>(string sp)
        {
            using IDbConnection db = _dbConnection;
            return await db.QueryAsync<T>(sp, commandType: CommandType.StoredProcedure);
        }
        public async Task<List<dynamic>> GetJsonData(string sp)
        {
            var result = (await _dbConnection.QueryAsync(sp));
            return (List<dynamic>)result;
        }

        public T Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = _dbConnection;
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
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
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return result;
        }
        public T Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = _dbConnection;
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
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
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return result;
        }
        public T Delete<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.Text)
        {
            T result;
            using IDbConnection db = _dbConnection;
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
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
                if (db.State == ConnectionState.Open)
                    db.Close();
            }
            return result;
        }

        public async Task<object> Execute(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = _dbConnection;
            var data = (await db.QueryAsync<string>(sp, parms, commandType: commandType)).FirstOrDefault();
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
    }
}
