using Microsoft.EntityFrameworkCore;

namespace Projeto.Service.DataInterfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext _dbContext { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
