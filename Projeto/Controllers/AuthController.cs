using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Models;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service;

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
           _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User request)
        {
            return await _userService.Register(request).GetAsyncResult();

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
