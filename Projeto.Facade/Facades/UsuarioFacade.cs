using AutoMapper;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Facade.Interfaces;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service;
using Projeto.Service.DTO;
using System.Net;

namespace Projeto.Facade.Facades
{
    public class UsuarioFacade : IUsuarioFacade
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _userService;

        public UsuarioFacade(IMapper mapper, IUsuarioService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Response<UsuarioViewModel>> Logar(UsuarioLoginViewModel user)
        {
            var response = new Response<UsuarioViewModel>();
            try
            {
                var result = await _userService.Logar(_mapper.Map<Usuario>(user));

                if (result.Status != HttpStatusCode.OK)
                {
                    response.Copy(result);
                }
                else if (result.Entity != null)
                {
                    response.Entity = _mapper.Map<UsuarioViewModel>(result.Entity);
                }
            }
            catch (Exception ex) { }
            return response;
        }

        public async Task<Response<UsuarioViewModel>> Autenticar(string login, string senha)
        {
            var response = new Response<UsuarioViewModel>();
            try
            {
                var result = await _userService.Autenticar(login, senha);

                if (result.Status != HttpStatusCode.OK)
                {
                    response.Copy(result);
                }
                else if (result.Entity != null)
                {
                    response.Entity = _mapper.Map<UsuarioViewModel>(result.Entity);
                }
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        public async Task<Response<UsuarioCadastroViewModel>> Cadastrar(UsuarioCadastroViewModel usuario)
        {
            var response = new Response<UsuarioCadastroViewModel>();

            try
            {
                var result = await _userService.Cadastrar(_mapper.Map<Usuario>(usuario));

                if (result.Status != HttpStatusCode.OK)
                {
                    response.Copy(result);
                }
                else if (result.Entity != null)
                {
                    response.Entity = _mapper.Map<UsuarioCadastroViewModel>(result.Entity);
                }
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }

            return response;
        }
    }
}
