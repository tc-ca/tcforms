using System;

namespace GoC.TC.Repositories
{ 
    public interface ITransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
