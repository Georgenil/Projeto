using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Models;
using Projeto.Service.DataInterfaces;
using Projeto.Service.Interfaces;
using Projeto.Utils.ExtensionMethod;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;

namespace Projeto.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<Response> Register(User user)
        {
            Response response = new Response();

            try
            {
                if (string.IsNullOrEmpty(user.Password)) return new Response(404, "Senha é obrigatória");

                user.GUID = Guid.NewGuid();
                user.Login = user.Login;
                user.Password = user.Password.ToSha256();
                user.Ativo = true;


                _userRepository.Add(user);

                int commited = await _unitOfWork.CommitAsync();

                if (commited > 0 && user.Id > 0)
                {
                    user.Password = null;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Content = user;
                    return new Response(200, user);
                }
                else
                {
                    throw new ApplicationException("An error occurred while creating the user");
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao registrar o usuário. Login: {user.Login}";
                response.Content = ex;
            }
            return response; 
        }
    }
}
