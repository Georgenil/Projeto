using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service.DTO;

namespace Projeto.Service
{
    public interface IUserService
    {
        Task<Response<UserDTO>> Login(User user);
        Task<Response<UserDTO>> Autenticar(string login, string senha);
        Task<Response<User>> Register(User user);
    }
}
