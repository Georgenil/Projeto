using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.ExtensionMethod;

namespace Projeto.Facade
{
    public interface IColaboradorFacade
    {
        Response<IList<ColaboradorViewModel>> BuscarTodos();
        Task<Response<ColaboradorViewModel>> Cadastrar(ColaboradorViewModel model);
        Task<Response<ColaboradorViewModel>> Editar(ColaboradorViewModel model);
        Task<Response<bool>> Remover(int id);
    }
}
