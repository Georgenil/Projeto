using Projeto.Domain.Models;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Utils.ExtensionMethod;
using System.Net;

namespace Projeto.Service
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

        public async Task<Response<User>> Register(User user)
        {
            var response = new Response<User>();

            try
            {
                if (string.IsNullOrEmpty(user.Password)) return new Response<User>(HttpStatusCode.NotFound, "Senha é obrigatória");

                user.GUID = Guid.NewGuid();
                user.Login = user.Login;
                user.Password = user.Password.ToSha256();
                user.Ativo = true;


                _userRepository.Add(user);

                int commited = await _unitOfWork.CommitAsync();

                if (commited > 0 && user.Id > 0)
                {
                    user.Password = null;
                    response.Status = HttpStatusCode.OK;
                    response.Entity = user;
                }
                else
                {
                    throw new ApplicationException("An error occurred while creating the user");
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao registrar o usuário. Login: {user.Login}";
            }
            return response; 
        }
    }
}
