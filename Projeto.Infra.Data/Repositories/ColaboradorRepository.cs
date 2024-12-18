using Projeto.Domain.Models;
using Projeto.Service;

namespace Projeto.Infra.Data.Repositories
{
    public class ColaboradorRepository : GenericRepository<Colaborador>, IColaboradorRepository
    {
        public ColaboradorRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
