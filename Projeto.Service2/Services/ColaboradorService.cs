using Projeto.Domain.DataInterfaces;
using Projeto.Application.Interfaces;
using Projeto.Domain.ViewModels;
using Projeto.Models;
using Projeto.Service.DataInterfaces;

namespace Projeto.Service.Services
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
        public async Task<Response> BuscarTodos()
        {
            try
            {
                var colaboradores = await _colaboradorRepository.GetAllAsync();

                if (colaboradores == null) return new Response(404, "Não existem colaboradores cadastrados no banco.");

                return new Response(200, colaboradores);
            }
            catch
            {
                return new Response(500, "Erro ao buscar colaboradores.");
            }
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
        //        return new Response(500, "Erro ao buscar colaborador.");
        //    }
        //}

        public async Task<Response> Cadastrar(ColaboradorViewModel model)
        {
            try
            {
                if (model == null) return new Response(404, "Colaborador nulo");

                var colaboradorEncontrado = await _colaboradorRepository.FindFirstByAsync(x => x.Nome.ToLower().Trim().Equals(model.Nome.ToLower().Trim()));

                if (colaboradorEncontrado != null) return new Response(500, "Este colaborador já foi cadastrado");

                Colaborador colaborador = new()
                {
                    Nome = model.Nome,
                };

                var colaboradorBd = _colaboradorRepository.Add(colaborador);
                _unitOfWork.Commit();

                return new Response(200, colaboradorBd);
            }
            catch
            {
                return new Response(500, "Erro ao cadastrar colaboradores.");
            }
        }

        public async Task<Response> Editar(ColaboradorViewModel model)
        {
            try
            {
                if (model == null) return new Response(500, "Informe os novos dados do colaborador");

                var colaboradorEncontrado = await _colaboradorRepository.FindFirstByAsync(x => x.Id == model.Id);

                if (colaboradorEncontrado == null) return new Response(500, "Não é possível encontrar o colaborador");

                colaboradorEncontrado.Nome = model.Nome;

                _colaboradorRepository.Edit(colaboradorEncontrado);
                await _unitOfWork.CommitAsync();

                return new Response(200, model);
            }
            catch
            {
                return new Response(500, "Erro ao atualizar colaboradores");

            }
        }

        public async Task<Response> Remover(int id)
        {
            try
            {
                var colaborador = await _colaboradorRepository.FindFirstByAsync(x => x.Id == id);

                if (colaborador == null) return new Response(500, "Colaborador não encontrado");


                _colaboradorRepository.Delete(colaborador);
                _unitOfWork.Commit();

                return new Response(200, "Colaborador removido com sucesso!");
            }
            catch
            {
                return new Response(500, "Erro ao exluir colaborador");
            }
        }
    }
}
