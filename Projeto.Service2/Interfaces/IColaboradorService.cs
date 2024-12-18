using Projeto.Domain.Models;
using Projeto.Infra.Utils.ExtensionMethod;

namespace Projeto.Service
{
    public interface IColaboradorService
    {
        Response<IList<Colaborador>> BuscarTodos();
        Task<Response<Colaborador>> Cadastrar(Colaborador model);
        Task<Response<Colaborador>> Editar(Colaborador model);
        Task<Response<bool>> Remover(int id);
    }
}
