using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Facade.Interfaces;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service;

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserFacade _userFacade;

        public AuthController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserViewModel request)
        {
            return await _userFacade.Register(request).GetAsyncResult();
        }

        [HttpPost("autenticar")]
        public async Task<IActionResult> Autenticar(UserLoginViewModel user)
        {
            return await _userFacade.Autenticar(user.Login, user.Password).GetAsyncResult();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            return await _userFacade.Login(user).GetAsyncResult();
        }

        [HttpGet("precisaEstarLogado")]
        [Authorize]
        public string PrecisaEstarLogado()
        {
            return $"Autenticado - {User.Identity.Name}";
        }

        private string GenerateTokenMethod(User user)
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
