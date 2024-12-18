using AutoMapper;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Facade.Interfaces;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service;
using System.Net;

namespace Projeto.Facade.Facades
{
    public class UserFacade : IUserFacade
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserFacade(IMapper mapper,IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<Response<UserViewModel>> Register(UserViewModel user)
        {
            var response = new Response<UserViewModel>();

            try
            {
                var result = await _userService.Register(_mapper.Map<User>(user));

                if (response.Status != HttpStatusCode.OK)
                {
                    response.Copy(result);
                }
                else if (response.Entity != null)
                {
                    response.Entity = _mapper.Map<UserViewModel>(response.Entity);
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}
