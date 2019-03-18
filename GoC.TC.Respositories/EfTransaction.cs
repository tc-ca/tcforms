using System.Data.Entity;

namespace GoC.TC.Repositories
{
    public class EfTransaction : ITransaction
    {
        private DbContextTransaction _transaction;

        public EfTransaction(DbContextTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
