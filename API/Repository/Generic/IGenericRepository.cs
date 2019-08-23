using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repository.Generic
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        Task<List<T>> GetAll();

        Task<List<T>> FindBy(Expression<Func<T, bool>> predicate);

        Task<T> Add(T entity, bool withTransaction = true);

        Task<int> Delete(T entity, bool withTransaction = true);

        Task<T> Edit(T entity, bool withTransaction = true);

        Task<R> ExecuteInTransaction<R>(Func<Task<R>> action);

        void ExecuteInTransaction(Action action);
    }
}
