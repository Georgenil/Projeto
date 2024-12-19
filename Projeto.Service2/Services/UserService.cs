using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.Configurations;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service.DTO;
using Projeto.Utils.ExtensionMethod;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Projeto.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<AppConfig> _config;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IOptions<AppConfig> config)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<Response<UserDTO>> Login(User user)
        {
            var response = new Response<UserDTO>() { Entity = new UserDTO()};

            try
            {
                string senhaEncriptada = user.Password.ToSha256();

                var userBD = _userRepository.FindFirstBy(u => u.Login == user.Login && u.Password == senhaEncriptada);
                if (userBD == null) return new Response<UserDTO>(message: "Usuário ou senha inválidos", status: HttpStatusCode.NotFound);

                var token = GenerateToken(user);
                userBD.Password = string.Empty;

                response.Entity.user = userBD ;

                response.Entity.Token = token;
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }

            return response;
        }


        public async Task<Response<UserDTO>> Autenticar(string login, string senha)
        {
            Response<UserDTO> response = new Response<UserDTO>();
            response.Status = HttpStatusCode.Unauthorized;

            try
            {
                string senhaEncriptada = senha.ToSha256();

                User user = _userRepository.FindFirstBy(u => u.Login == login && u.Ativo);

                if (user == null) return new Response<UserDTO> { Status = HttpStatusCode.Unauthorized, Message = "Usuário ou senha inválidos" };

                response = ObterUsuarioLogadoDTO(user, login);

                _userRepository.Edit(user);

                await _unitOfWork.CommitAsync();

            }
            catch (Exception e)
            {
                response.Exception(e);
            }

            return response;
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

        public Response<UserDTO> ObterUsuarioLogadoDTO(User user, string login)
        {
            Response<UserDTO> response = new Response<UserDTO>();

            try
            {
                string token = GenerateToken(user);
                long validade = new DateTimeOffset(DateTime.UtcNow.AddMinutes(_config.Value.MinutosJanelaReativacaoToken)).ToUnixTimeSeconds();

                response.Entity = new UserDTO
                {
                    user = user,
                    Token = token,
                    TokenAtualizacao = ObterTokenAtualizacao(login, token, validade),
                    TokenValidade = validade
                };

                response.Status = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                response.Exception(e);
            }

            return response;
        }

        public string GenerateToken(User user)
        {
            string token = string.Empty;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.Value.ChaveSecreta);

                IList<Claim> claims = new List<Claim>
                {
                      new Claim(ClaimTypes.Name, user.Login.ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims.ToArray()),
                    Expires = DateTime.UtcNow.AddMinutes(_config.Value.MinutosValidadeTokenAcesso),
                    NotBefore = DateTime.UtcNow,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            }
            catch (Exception e)
            {
                throw new Exception(message: e.Message);
            }

            return token;
        }

        private string ObterTokenAtualizacao(string login, string token, long validade)
        {
            return $"{login}{token}{validade}{_config.Value.ChaveSecreta}".ToSha256();
        }
    }
}
