using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GoC.TC.Repositories
{
    public class EfRepository : IRepository, IAsyncRepository
    {
        private readonly DbContext _dbContext;

        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;

            _dbContext.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public T GetOne<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return Get(predicate).FirstOrDefault();
        }

        public IList<T> Get<T>(Expression<Func<T, bool>> predicate = null) where T : class
        {
            return predicate == null
                ? _dbContext.Set<T>().ToList()
                : _dbContext.Set<T>().Where(predicate).ToList();
        }

        public T Add<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Update<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public int NextSequence<T>() where T : class
        {
            var tablename = GetTableName<T>();
            return _dbContext.Database.SqlQuery<int>($"SELECT {tablename}_seq.nextval FROM dual").First();
        }


        public async Task<int> NextSequenceAsync<T>() where T : class
        {
            var tablename = GetTableName<T>();
            return await _dbContext.Database.SqlQuery<int>($"SELECT {tablename}_seq.nextval FROM dual").FirstAsync();
        }


        public async Task<T> GetOneAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return (await GetAsync(predicate)).FirstOrDefault();
        }

        public async Task<IList<T>> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return predicate == null
                ? await _dbContext.Set<T>().ToListAsync()
                : await _dbContext.Set<T>().Where(predicate).AsQueryable().ToListAsync();
        }

        public ITransaction BeginTransaction()
        {
            return new EfTransaction(_dbContext.Database.BeginTransaction());
        }

        private static string GetTableName<T>()
        {
            var tableAttr = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
            return tableAttr?.Name;
        }
    }
}
