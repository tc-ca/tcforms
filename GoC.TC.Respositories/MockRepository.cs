using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GoC.TC.Repositories
{
    public class MockRepository : IRepository
    {
        private readonly IList<object> _data;
        private readonly Random _rng = new Random();

        public MockRepository(params object[] data)
        {
            _data = data.ToList();
        }

        public T GetOne<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Get(predicate).FirstOrDefault();
        }

        public IList<T> Get<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return predicate == null
                ? _data.OfType<T>().ToList()
                : _data.OfType<T>().AsQueryable().Where(predicate).ToList();
        }

        public T Add<T>(T entity) where T : class
        {
            _data.Add(entity);
            return entity;
        }

        public void Update<T>(T entity) where T : class { }

        public void Delete<T>(T entity) where T : class
        {
            _data.Remove(entity);
        }

        public int NextSequence<T>() where T : class
        {
            return _rng.Next(1000);
        }

        public ITransaction BeginTransaction()
        {
            return new MockTransaction();
        }
    }
}