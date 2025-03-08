using Projeto.Domain.Models;
using Projeto.Infra.Data.Interfaces;

namespace Projeto.Infra.Data.Repositories
{
    public class ColaboradorRepository : GenericRepository<Colaborador>, IColaboradorRepository
    {
        public ColaboradorRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    }
}
