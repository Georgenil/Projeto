using Projeto.Domain.Models;
using Projeto.Infra.Data.Interfaces;

namespace Projeto.Infra.Data.Repositories
{
    public class ArquivoRepository : GenericRepository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}