﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repository.Generic
{
    public class GenericRepository<C, T> : IGenericRepository<T>, IDisposable where C : DbContext where T : class
    {
        protected C Context { get; }

        protected DbSet<T> Data { get; }

        public GenericRepository(C context)
        {
            Context = context;
            Data = Context.Set<T>();
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await Data.ToListAsync();
        }

        public virtual async Task<List<T>> FindBy(Expression<Func<T, bool>> predicate)
        {
            return await Data.AsQueryable().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> Add(T entity, bool withTransaction = true)
        {
            if (withTransaction)
            {
                async Task<T> action()
                {
                    return await Create(entity);
                };

                return await ExecuteInTransaction(action);
            }
            else
            {
                return await Create(entity);
            }
        }

        private async Task<T> Create(T entity)
        {
            if (Context == null)
            {
                throw new ArgumentException("Null Context");
            }
            if (entity == null)
            {
                throw new ArgumentException("Null Parameter");
            }

            await Data.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;

        }


        public virtual async Task<int> Delete(T entity, bool withTransaction = true)
        {
            if (withTransaction)
            {

                async Task<int> action()
                {
                    return await Remove(entity);
                };

                return await ExecuteInTransaction(action);
            }
            else
            {
                return await Remove(entity);
            }
        }

        private async Task<int> Remove(T entity)
        {
            if (Context == null)
            {
                throw new ArgumentException("Null Context");
            }
            if (entity == null)
            {
                throw new ArgumentException("Null Parameter");
            }
            Data.Remove(entity);
            return await Context.SaveChangesAsync();

        }

        public virtual async Task<T> Edit(T entity, bool withTransaction = true)
        {
            if (withTransaction)
            {
                async Task<T> action()
                {
                    return await Update(entity);
                };

                return await ExecuteInTransaction(action);
            }
            else
            {
                return await Update(entity);
            }
        }

        private async Task<T> Update(T entity)
        {
            if (Context == null)
            {
                throw new ArgumentException("Null Context");
            }
            if (entity == null)
            {
                throw new ArgumentException("Null Parameter");
            }
            Data.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }


        public virtual void Dispose()
        {
            Context.Dispose();
        }

        public async Task<R> ExecuteInTransaction<R>(Func<Task<R>> action)
        {
            if (Context.Database.IsSqlServer())
            {
                using (var transaction = Context.Database.BeginTransaction())
                {
                    try
                    {
                        R entity = await action();
                        transaction.Commit();
                        return entity;
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            else if (Context.Database.IsInMemory())
            {
                return await action();
            }
            else
            {
                //do nothing
                return default;
            }
        }


        public void ExecuteInTransaction(Action action)
        {
            if (Context.Database.IsSqlServer())
            {
                var currentTransaction = Context.Database.CurrentTransaction;
                if (currentTransaction == null)
                {
                    using (var transaction = Context.Database.BeginTransaction())
                    {
                        try
                        {
                            action.Invoke();
                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
                else
                {
                    //transaction already run
                    using (var transaction = currentTransaction)
                    {
                        try
                        {
                            action.Invoke();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }


            }
            else if (Context.Database.IsInMemory())
            {
                action.Invoke();
            }
            else
            {
                //do nothing
            }
        }
    }
}
