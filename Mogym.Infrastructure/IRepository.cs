using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Mogym.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity, bool withSaveChange = true);
        void Add(TEntity entity, bool withSaveChange = true);
        void AddRang(IEnumerable<TEntity> entityListEnumerable, bool withSaveChange = true);
        Task AddRangAsync(IEnumerable<TEntity> entityListEnumerable, bool withSaveChange = true);
        void Delete(TEntity entity, bool withSaveChange = true);
        void DeleteRange(IEnumerable<TEntity> entityListEnumerable, bool withSaveChange = true);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity? GetById(int id);
        Task<TEntity?> GetByIdAsync(int id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity entity, bool withSaveChange = true);
        void UpdateRange(IEnumerable<TEntity> entityListEnumerable, bool withSaveChange = true);
        Task UpdateAsync(TEntity entity, bool withSaveChange = true);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    }
}
