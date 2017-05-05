using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Data.EntiyRepositories
{
    public interface IEntityRepository<T> where T : class, new()
    {
        IQueryable<T> GetAll();

        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        PaginatedList<T> Paginate<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector);

        PaginatedList<T> Paginate<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity);

        void Edit(T entity);

        void Delete(T entity);

        void UpdateRange(List<T> entities);

        void InsertRange(List<T> entities, int batchSize = 100);

        void DeleteRange(List<T> entities);

        /// <summary>
        /// SUBMIT CHANGES dataContext
        /// </summary>
        void Save();
    }
}