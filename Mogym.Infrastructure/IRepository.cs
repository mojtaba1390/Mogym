using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Mogym.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        void Add(TEntity entity);
        void AddRang(IEnumerable<TEntity> entityListEnumerable);
        Task AddRangAsync(IEnumerable<TEntity> entityListEnumerable);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entityListEnumerable);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity? GetById(int id);
        Task<TEntity?> GetByIdAsync(int id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entityListEnumerable);
    }
}
