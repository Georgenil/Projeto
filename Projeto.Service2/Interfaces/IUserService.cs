using Projeto.Domain.Models;
using Projeto.Infra.Utils.ExtensionMethod;

namespace Projeto.Service
{
    public interface IUserService
    {
        Task<Response<User>> Register(User user);
    }
}
