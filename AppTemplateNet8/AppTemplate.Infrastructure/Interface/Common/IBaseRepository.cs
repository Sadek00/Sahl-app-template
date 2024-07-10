using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppTemplate.Infrastructure.Interface.Common
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> Entities
        {
            get;
        }

        #region GetMethods
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsyncNoTracking();

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsyncNoTracking(CancellationToken cancellationToken);

        IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,string? includeProperties = null, int? skip = null, int? take = null);
        IEnumerable<T> GetAllNoTracking(Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int? skip = null, int? take = null);

        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,string? includeProperties = null, int? skip = null, int? take = null);
        Task<IEnumerable<T>> GetAllAsyncNoTracking(Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int? skip = null, int? take = null);

        IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null,Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,string? includeProperties = null, int? skip = null, int? take = null);
        IEnumerable<T> GetNoTracking(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int? skip = null, int? take = null);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int? skip = null, int? take = null);
        Task<IEnumerable<T>> GetAsyncNoTracking(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int? skip = null, int? take = null);

        T GetOne(Expression<Func<T, bool>>? filter = null, string includeProperties = "");
        T GetOneNoTracking(Expression<Func<T, bool>>? filter = null, string includeProperties = "");

        Task<T> GetOneAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> GetOneAsyncNoTracking(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        T GetFirst(Expression<Func<T, bool>>? filter = null,Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        T GetFirstNoTracking(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");

        Task<T> GetFirstAsync(Expression<Func<T, bool>>? filter = null,Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,string? includeProperties = null);
        Task<T> GetFirstAsyncNoTracking(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null);

        bool GetExists(Expression<Func<T, bool>>? filter = null);
        bool GetExistsNoTracking(Expression<Func<T, bool>>? filter = null);

        Task<bool> GetExistsAsync(Expression<Func<T, bool>>? filter = null);
        Task<bool> GetExistsAsyncNoTracking(Expression<Func<T, bool>>? filter = null);

        IQueryable<T> GetQueryable(Expression<Func<T, bool>>? filter = null,Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,string? includeProperties = null,int? skip = null,int? take = null);
        IQueryable<T> GetQueryableNoTracking(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null, int? skip = null, int? take = null);
        #endregion

        #region Crud Operations
        void AddRange(IEnumerable<T> entity);

        void AddRangeAsync(IEnumerable<T> entity);

        void Add(T entity);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);

        void DetachEntry(T entity);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);

        void Save();

        Task<long> SaveAsync();
        #endregion

        #region FindMethods
        T Find(Expression<Func<T, bool>> match);
        T FindNoTracking(Expression<Func<T, bool>> match);

        Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsyncNoTracking(Expression<Func<T, bool>> match);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> match);
        IEnumerable<T> FindAllNoTracking(Expression<Func<T, bool>> match);

        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<List<T>> FindAllAsyncNoTracking(Expression<Func<T, bool>> match);

        Task<IEnumerable<T>> FindAllIEnumerableAsync(Expression<Func<T, bool>> match);
        Task<IEnumerable<T>> FindAllIEnumerableAsyncNoTracking(Expression<Func<T, bool>> match);
        #endregion

        void Dispose();
    }
}
