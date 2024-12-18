using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data;
using Projeto.Service.DataInterfaces;

namespace Projeto.Infra.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext _dbContext { get; set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}
