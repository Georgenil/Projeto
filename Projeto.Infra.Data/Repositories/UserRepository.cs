using Projeto.Domain.Models;
using Projeto.Service.DataInterfaces;

namespace Projeto.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
