using Projeto.Domain.ViewModels;

namespace Projeto.Application.Interfaces
{
    public interface IColaboradorService
    {
        Task<Response> BuscarTodos();
        Task<Response> Cadastrar(ColaboradorViewModel model);
        Task<Response> Editar(ColaboradorViewModel model);
        Task<Response> Remover(int id);
    }
}
