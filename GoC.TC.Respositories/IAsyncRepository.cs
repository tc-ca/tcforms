using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GoC.TC.Repositories
{
    public interface IAsyncRepository : IBeginTransaction
    {
        Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<IList<T>> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> AddAsync<T>(T entity) where T : class;
        Task UpdateAsync<T>(T entity) where T : class;
        Task DeleteAsync<T>(T entity) where T : class;
        Task<int> NextSequenceAsync<T>() where T : class;
    }
}
