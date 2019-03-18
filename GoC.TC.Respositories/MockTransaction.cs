namespace GoC.TC.Repositories
{
    public class MockTransaction : ITransaction
    {
        public void Dispose() { }

        public void Commit() { }

        public void Rollback() { }
    }
}
