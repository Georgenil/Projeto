using Projeto.Domain.Models;
using Projeto.Infra.Data.Interfaces;

namespace Projeto.Infra.Data.Interfaces
{
    public interface IUserRepository : IGenericRepository<Usuario>
    {
    }
}
