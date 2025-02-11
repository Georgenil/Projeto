using AutoMapper;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service;
using System.Net;

namespace Projeto.Facade
{
    public class ColaboradorFacade : IColaboradorFacade
    {
        private readonly IMapper _mapper;
        private readonly IColaboradorService _colaboradorService;
        public ColaboradorFacade(IMapper mapper, IColaboradorService colaboradorService)
        {
            _mapper = mapper;
            _colaboradorService = colaboradorService;
        }

        public async Task<Response<IList<ColaboradorViewModel>>> BuscarTodos()
        {
            var response = new Response<IList<ColaboradorViewModel>>();

            try
            {
                var resultAsync = _colaboradorService.BuscarTodos();

                if (resultAsync.Result.Status != HttpStatusCode.OK)
                {
                    response.Copy(resultAsync.Result);
                }
                else
                {
                    response.Entity = _mapper.Map<IList<ColaboradorViewModel>>(resultAsync.Result.Entity);
                    response.Status = HttpStatusCode.OK;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Exception(e);
            }

            return response;
        }

        public async Task<Response<ColaboradorViewModel>> Cadastrar(ColaboradorViewModel model)
        {
            var response = new Response<ColaboradorViewModel>();

            try
            {
                var result = _colaboradorService.Cadastrar(_mapper.Map<Colaborador>(model));

                if (result.Result.Status != HttpStatusCode.OK)
                {
                    response.Copy(result.Result);
                }
                else
                {
                    response.Entity = _mapper.Map<ColaboradorViewModel>(result.Result.Entity);
                    response.Status = HttpStatusCode.OK;
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Exception(e);
            }
            return response;
        }

        public Task<Response<ColaboradorViewModel>> Editar(ColaboradorViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<Response<bool>> Remover(int id)
        {
            throw new NotImplementedException();
        }
    }
}
