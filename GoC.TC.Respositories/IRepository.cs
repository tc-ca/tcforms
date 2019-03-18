using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoC.TC.Repositories
{
    public interface IRepository : IBeginTransaction
    {
        T GetOne<T>(Expression<Func<T, bool>> predicate) where T : class;
        IList<T> Get<T>(Expression<Func<T, bool>> predicate = null) where T : class;
        T Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        int NextSequence<T>() where T : class;
    }
}
