using Microsoft.EntityFrameworkCore;

namespace Projeto.Infra.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext _dbContext { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
