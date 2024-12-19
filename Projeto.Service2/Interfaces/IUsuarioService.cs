using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service.DTO;

namespace Projeto.Service
{
    public interface IUsuarioService
    {
        Task<Response<UsuarioDTO>> Logar(Usuario user);
        Task<Response<UsuarioDTO>> Autenticar(string login, string senha);
        Task<Response<Usuario>> Cadastrar(Usuario user);
    }
}
