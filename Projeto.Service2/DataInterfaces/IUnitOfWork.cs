using Microsoft.EntityFrameworkCore;

namespace Projeto.Service
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext _dbContext { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
