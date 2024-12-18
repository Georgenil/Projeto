using Projeto.Domain.DataInterfaces;
using Projeto.Models;
using Projeto.Service.DataInterfaces;

namespace Projeto.Infra.Data.Repositories
{
    public class ColaboradorRepository : GenericRepository<Colaborador>, IColaboradorRepository
    {
        public ColaboradorRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
