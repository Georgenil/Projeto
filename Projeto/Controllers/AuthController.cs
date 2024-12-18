using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using System.Security.Cryptography;

namespace Projeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserViewModel request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash);

            user.Login = request.Login;
            user.PasswordHash = passwordHash;

            return Ok(user);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
