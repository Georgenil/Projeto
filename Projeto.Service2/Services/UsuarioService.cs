using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Projeto.Domain.Models;
using Projeto.Infra.Data.Interfaces;
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
    public class UsuarioService : IUsuarioService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<AppConfig> _config;

        public UsuarioService(IUnitOfWork unitOfWork, IUserRepository userRepository, IOptions<AppConfig> config)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _config = config;
        }

        public async Task<Response<UsuarioDTO>> Logar(Usuario user)
        {
            var response = new Response<UsuarioDTO>() { Entity = new UsuarioDTO() };

            try
            {
                string senhaEncriptada = user.Senha.ToSha256();

                var userBD = _userRepository.FindFirstBy(u => u.Login == user.Login && u.Senha == senhaEncriptada);
                if (userBD == null) return new Response<UsuarioDTO>(message: "Usuário ou senha inválidos", status: HttpStatusCode.NotFound);

                var token = GerarToken(user);
                userBD.Senha = string.Empty;

                response.Entity.usuario = userBD;

                response.Entity.Token = token;
            }
            catch (Exception ex)
            {
                response.Exception(ex);
            }

            return response;
        }


        public async Task<Response<UsuarioDTO>> Autenticar(string login, string senha)
        {
            Response<UsuarioDTO> response = new Response<UsuarioDTO>();
            response.Status = HttpStatusCode.Unauthorized;

            try
            {
                string senhaEncriptada = senha.ToSha256();

                Usuario user = _userRepository.FindFirstBy(u => u.Login == login && u.Ativo);

                if (user == null) return new Response<UsuarioDTO> { Status = HttpStatusCode.Unauthorized, Message = "Usuário ou senha inválidos" };

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

        public async Task<Response<Usuario>> Cadastrar(Usuario Usuario)
        {
            var response = new Response<Usuario>();
            var messages = new List<string>();

            try
            {
                if (string.IsNullOrEmpty(Usuario.Login))
                {
                    messages.Add("Login é obrigatória".TranslateTo(CultureInfo.CurrentCulture.ToString()));
                }

                if (string.IsNullOrEmpty(Usuario.Senha))
                {
                    messages.Add("Senha é obrigatória");
                }

                if (messages.Count > 0)
                {
                    response.Status = HttpStatusCode.BadRequest;
                    response.Messages = messages.ToArray();
                    return response;
                }

                Usuario.GUID = Guid.NewGuid();
                Usuario.Login = Usuario.Login;
                Usuario.Senha = Usuario.Senha.ToSha256();
                Usuario.Ativo = true;


                _userRepository.Add(Usuario);

                int commited = await _unitOfWork.CommitAsync();

                if (commited > 0 && Usuario.Id > 0)
                {
                    Usuario.Senha = null;
                    response.Status = HttpStatusCode.OK;
                    response.Entity = Usuario;
                }
                else
                {
                    throw new ApplicationException("An error occurred while creating the usuario");
                }
            }
            catch (Exception ex)
            {
                response.Message = $"Ocorreu um erro ao registrar o usuário. Login: {Usuario.Login}";
            }
            return response;
        }

        public Response<UsuarioDTO> ObterUsuarioLogadoDTO(Usuario user, string login)
        {
            Response<UsuarioDTO> response = new Response<UsuarioDTO>();

            try
            {
                string token = GerarToken(user);
                long validade = new DateTimeOffset(DateTime.UtcNow.AddMinutes(_config.Value.MinutosJanelaReativacaoToken)).ToUnixTimeSeconds();

                response.Entity = new UsuarioDTO
                {
                    usuario = user,
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

        public string GerarToken(Usuario user)
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
