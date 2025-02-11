using Projeto.Domain.Models;
using Projeto.Infra.Utils.ExtensionMethod;
using System.Net;

namespace Projeto.Service
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly IColaboradorRepository _colaboradorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ColaboradorService(IUnitOfWork unitOfWork, IColaboradorRepository colaboradorRepository)
        {
            _unitOfWork = unitOfWork;
            _colaboradorRepository = colaboradorRepository;
        }
        public async Task<Response<IList<Colaborador>>> BuscarTodos()
        {
            var response = new Response<IList<Colaborador>>();

            try
            {
                response.Entity = await _colaboradorRepository.GetAllAsync();
            }
            catch
            {
                return new Response<IList<Colaborador>>(HttpStatusCode.InternalServerError, "Erro ao buscar fornecedores.");
            }

            return response;
        }

        //public async Task<Response> BuscarPorId(int id)
        //{
        //    try
        //    {
        //        var colaboradorEncontrado = await _context.Colaboradores.Include(x => x.Cargo).Include(x => x.Setor).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        //        if (colaboradorEncontrado == null) return new Response(404, "Não existem colaboradores cadastrados no banco.");

        //        return new Response(200, colaboradorEncontrado);
        //    }
        //    catch
        //    {
        //        return new Response(HttpStatusCode.InternalServerError, "Erro ao buscar colaborador.");
        //    }
        //}

        public async Task<Response<Colaborador>> Cadastrar(Colaborador colaborador)
        {
            var response = new Response<Colaborador>();
            try
            {
                if (colaborador == null) return new Response<Colaborador>(HttpStatusCode.NotFound, "Colaborador nulo");

                var colaboradorBD = await _colaboradorRepository.FindFirstByAsync(x => x.Nome.ToLower().Trim().Equals(colaborador.Nome.ToLower().Trim()));

                if (colaboradorBD != null) return new Response<Colaborador>(HttpStatusCode.InternalServerError, "Este colaborador já foi cadastrado");


                var colaboradorAdicionado = await _colaboradorRepository.AddAsync(colaborador);
                await _unitOfWork.CommitAsync();

                response.Entity = colaboradorAdicionado;
            }
            catch
            {
                return new Response<Colaborador>(HttpStatusCode.InternalServerError, "Erro ao cadastrar colaboradores.");
            }

            return response;
        }

        public async Task<Response<Colaborador>> Editar(Colaborador model)
        {
            var response = new Response<Colaborador>();

            try
            {
                if (model == null) return new Response<Colaborador>(HttpStatusCode.InternalServerError, "Informe os novos dados do colaborador");

                var colaboradorBD = await _colaboradorRepository.FindFirstByAsync(x => x.Id == model.Id);

                if (colaboradorBD == null) return new Response<Colaborador>(HttpStatusCode.InternalServerError, "Não é possível encontrar o colaborador");

                colaboradorBD.Nome = model.Nome;

                _colaboradorRepository.Edit(colaboradorBD);
                await _unitOfWork.CommitAsync();

                response.Entity = colaboradorBD;
            }
            catch
            {
                return new Response<Colaborador>(HttpStatusCode.InternalServerError, "Erro ao atualizar colaboradores");

            }
            return response;
        }

        public async Task<Response<bool>> Remover(int id)
        {
            var response = new Response<bool>();

            try
            {
                var colaborador = await _colaboradorRepository.FindFirstByAsync(x => x.Id == id);

                if (colaborador == null) return new Response<bool>(HttpStatusCode.InternalServerError, "Colaborador não encontrado");


                _colaboradorRepository.Delete(colaborador);
                await _unitOfWork.CommitAsync();

                response.Entity = true;
                response.Status = HttpStatusCode.OK;
                response.Message = "Colaborador removido com sucesso!";
            }
            catch
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, "Erro ao exluir colaborador");
            }

            return response;
        }
    }
}
