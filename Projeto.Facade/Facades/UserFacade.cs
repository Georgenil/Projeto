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
    public class UserFacade : IUserFacade
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserFacade(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<Response<UserViewModel>> Login(UserLoginViewModel user)
        {
            var response = new Response<UserViewModel>();
            try
            {
                var result = await _userService.Login(_mapper.Map<User>(user));

                if (result.Status != HttpStatusCode.OK)
                {
                    response.Copy(result);
                }
                else if (result.Entity != null)
                {
                    response.Entity = _mapper.Map<UserViewModel>(result.Entity);
                }
            }
            catch (Exception ex) { }
            return response;
        }

        public async Task<Response<UserViewModel>> Autenticar(string login, string senha)
        {
            var response = new Response<UserViewModel>();
            try
            {
                var result = await _userService.Autenticar(login, senha);

                if (result.Status != HttpStatusCode.OK)
                {
                    response.Copy(result);
                }
                else if (result.Entity != null)
                {
                    response.Entity = _mapper.Map<UserViewModel>(result.Entity);
                }
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }
            return response;
        }

        public async Task<Response<UserViewModel>> Register(UserViewModel user)
        {
            var response = new Response<UserViewModel>();

            try
            {
                var result = await _userService.Register(_mapper.Map<User>(user));

                if (result.Status != HttpStatusCode.OK)
                {
                    response.Copy(result);
                }
                else if (result.Entity != null)
                {
                    response.Entity = _mapper.Map<UserViewModel>(result.Entity);
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
