using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Repository.Generic
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> FindBy(Expression<Func<T, bool>> predicate);

        Task<T> Add(T entity, bool withTransaction = true);

        Task<int> Delete(T entity, bool withTransaction = true);

        Task<T> Edit(T entity, bool withTransaction = true);

        Task<TR> ExecuteInTransaction<TR>(Func<Task<TR>> action);

        void ExecuteInTransaction(Action action);
    }
}
