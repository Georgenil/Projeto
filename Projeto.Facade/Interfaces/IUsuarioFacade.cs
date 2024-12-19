using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service.DTO;

namespace Projeto.Facade.Interfaces
{
    public interface IUsuarioFacade
    {
        Task<Response<UsuarioViewModel>> Logar(UsuarioLoginViewModel user);
        Task<Response<UsuarioViewModel>> Autenticar(string login, string senha);
        Task<Response<UsuarioCadastroViewModel>> Cadastrar(UsuarioCadastroViewModel user);
    }
}
