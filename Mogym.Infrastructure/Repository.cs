﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Mogym.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                await _dbContext.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.CommitTransactionAsync();
            }
            catch (DbUpdateException dbException)
            {
                await LogDbExceptionError(dbException);
                await _dbContext.Database.RollbackTransactionAsync();
            }
        }


        public void Add(TEntity entity)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            catch (DbUpdateException dbException)
            {
                 LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();
            }
        }
        public void AddRang(IEnumerable<TEntity> entityListEnumerable)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.AddRange(entityListEnumerable);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
            }
            catch (DbUpdateException dbException)
            {
                 LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();
            }
        }
        public async Task AddRangAsync(IEnumerable<TEntity> entityListEnumerable)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                await _dbContext.AddRangeAsync(entityListEnumerable);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.CommitTransactionAsync();
            }
            catch (DbUpdateException dbException)
            {
                await LogDbExceptionError(dbException);

                await _dbContext.Database.RollbackTransactionAsync();
            }
        }



        public void Delete(TEntity entity)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Set<TEntity>().Remove(entity);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();

            }
            catch (DbUpdateException dbException)
            {
                 LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();

            }

        }
        public void DeleteRange(IEnumerable<TEntity> entityListEnumerable)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Set<TEntity>().RemoveRange(entityListEnumerable);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();

            }
            catch (DbUpdateException dbException)
            {
                 LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();

            }

        }







        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().AsQueryable();
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





        public void Update(TEntity entity)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Set<TEntity>().Update(entity);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();

            }
            catch (DbUpdateException dbException)
            {
                 LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();

            }
        }
        public void UpdateRange(IEnumerable<TEntity> entityListEnumerable)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbContext.Set<TEntity>().UpdateRange(entityListEnumerable);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();

            }
            catch (DbUpdateException dbException)
            {
                  LogDbExceptionError(dbException);

                _dbContext.Database.RollbackTransaction();

            }
        }


        private Task LogDbExceptionError(DbUpdateException dbException)
        {
            foreach (var entity in dbException.Entries)
            {
                //log here
                var message = $"error in:"+ entity.GetType().Name;
            }

            return Task.CompletedTask;
        }


    }
}