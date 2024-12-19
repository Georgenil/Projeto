using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Facade.Interfaces;
using Projeto.Infra.Utils.ExtensionMethod;

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioFacade _userFacade;

        public AuthController(IUsuarioFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Cadastrar(UsuarioCadastroViewModel usuario)
        {
            return await _userFacade.Cadastrar(usuario).GetAsyncResult();
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar(UsuarioLoginViewModel user)
        {
            return await _userFacade.Autenticar(user.Login, user.Senha).GetAsyncResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Logar(UsuarioLoginViewModel user)
        {
            return await _userFacade.Logar(user).GetAsyncResult();
        }

        [HttpGet("precisaEstarLogado")]
        [Authorize]
        public string PrecisaEstarLogado()
        {
            return $"Autenticado - {User.Identity.Name}";
        }

        private string GenerateTokenMethod(Usuario user)
        {
            return "";
        }
        //private void CreatePasswordHash(string password, out byte[] passwordHash)
        //{
        //    using(var hmac = new HMACSHA512())
        //    {
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}
    }
}
