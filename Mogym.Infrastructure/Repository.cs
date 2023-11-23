using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Mogym.Infrastructure.Interfaces.Log;
using Mogym.Domain.Entities.Log;

namespace Mogym.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity,bool withSaveChange=true)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                await _dbContext.AddAsync(entity);
                if (withSaveChange)
                {
                    await _dbContext.SaveChangesAsync();
                }
                await _dbContext.Database.CommitTransactionAsync();
                return entity;
            }
            catch (DbUpdateException dbException)
            {
                await _dbContext.Database.RollbackTransactionAsync();
            }

            return null;
        }


        public void Add(TEntity entity, bool withSaveChange = true)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Add(entity);
                if (withSaveChange)
                {
                     _dbContext.SaveChanges();
                }
                _dbContext.Database.CommitTransaction();

            }
            catch (DbUpdateException dbException)
            {
                 //LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();
            }
        }
        public void AddRang(IEnumerable<TEntity> entityListEnumerable, bool withSaveChange = true)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.AddRange(entityListEnumerable);
                if (withSaveChange)
                {
                    _dbContext.SaveChanges();
                }
                _dbContext.Database.CommitTransaction();

            }
            catch (DbUpdateException dbException)
            {
                 //LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();
            }
        }
        public async Task AddRangAsync(IEnumerable<TEntity> entityListEnumerable, bool withSaveChange = true)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                await _dbContext.AddRangeAsync(entityListEnumerable);
                if (withSaveChange)
                {
                    await _dbContext.SaveChangesAsync();
                }
                await _dbContext.Database.CommitTransactionAsync();

            }
            catch (DbUpdateException dbException)
            {

                await _dbContext.Database.RollbackTransactionAsync();
            }
        }



        public void Delete(TEntity entity, bool withSaveChange = true)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Set<TEntity>().Remove(entity);
                if (withSaveChange)
                {
                    _dbContext.SaveChanges();
                }
                _dbContext.Database.CommitTransaction();


            }
            catch (DbUpdateException dbException)
            {
                 //LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();

            }

        }


        public void DeleteRange(IEnumerable<TEntity> entityListEnumerable, bool withSaveChange = true)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Set<TEntity>().RemoveRange(entityListEnumerable);
                if (withSaveChange)
                {
                    _dbContext.SaveChanges();
                }
                _dbContext.Database.CommitTransaction();


            }
            catch (DbUpdateException dbException)
            {
                 //LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();

            }

        }







        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().AsQueryable();
        }
        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).AsQueryable();
        }


        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
        }





        public void Update(TEntity entity, bool withSaveChange = true)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Set<TEntity>().Update(entity);
                if (withSaveChange)
                {
                    _dbContext.SaveChanges();
                }
                _dbContext.Database.CommitTransaction();


            }
            catch (DbUpdateException dbException)
            {
                foreach (var entry in dbException.Entries)
                {
                    if (entry.Entity is TEntity)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];

                            // TODO: decide which value should be written to database
                            // proposedValues[property] = <value to be saved>;
                        }

                        // Refresh original values to bypass next concurrency check
                        entry.OriginalValues.SetValues(databaseValues);
                    }
                    else
                    {
                        throw new NotSupportedException(
                            "Don't know how to handle concurrency conflicts for "
                            + entry.Metadata.Name);
                    }
                }





                //LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();

            }
        }
        public async Task UpdateAsync(TEntity entity, bool withSaveChange = true)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                _dbContext.Entry(entity).State = EntityState.Modified;

                if (withSaveChange)
                {
                    await _dbContext.SaveChangesAsync();
                }
                await _dbContext.Database.CommitTransactionAsync();


            }
            catch (DbUpdateException dbException)
            {
                //LogDbExceptionError(dbException);

                await _dbContext.Database.RollbackTransactionAsync();

            }
        }
        public void UpdateRange(IEnumerable<TEntity> entityListEnumerable, bool withSaveChange = true)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Set<TEntity>().UpdateRange(entityListEnumerable);
                if (withSaveChange)
                {
                    _dbContext.SaveChanges();
                }
                _dbContext.Database.CommitTransaction();

            }
            catch (DbUpdateException dbException)
            {
                 // LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();

            }
        }


        private Task LogDbExceptionError(DbUpdateException dbException)
        {
            foreach (var entry in dbException.Entries)
            {
                //log here
                var message = $"error in:"+ entry.GetType().Name;
                SeriLog serilog = new SeriLog()
                {
                    Exception = dbException.InnerException.Message,
                    Message = message,
                    Level = "Error",
                    MessageTemplate = message
                };
                _dbContext.Set<SeriLog>().Add(serilog);
                _dbContext.SaveChanges();
            }

            return Task.CompletedTask;
        }


    }
}
