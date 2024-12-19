using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service.DTO;

namespace Projeto.Facade.Interfaces
{
    public interface IUserFacade
    {
        Task<Response<UserViewModel>> Login(UserLoginViewModel user);
        Task<Response<UserViewModel>> Autenticar(string login, string senha);
        Task<Response<UserViewModel>> Register(UserViewModel user);
    }
}
