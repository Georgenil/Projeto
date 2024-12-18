using Projeto.Domain.ViewModels;
using Projeto.Service.DataInterfaces;
using Projeto.Service.Interfaces;
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

        public async Task<Response> Register(UserViewModel user)
        {

            CreatePasswordHash(user.Password, out byte[] passwordHash);

            user.GUID = Guid.NewGuid();
            user.Login = user.Login;
            user.Password = passwordHash;
            user.Ativo = true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
