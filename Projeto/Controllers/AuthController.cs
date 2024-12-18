using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Models;
using Projeto.Domain.Utils;
using Projeto.Domain.ViewModels;
using Projeto.Service.Interfaces;
using System.Security.Cryptography;

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
            return new ResponseHelper().CreateResponse(await _userService.Register(request));

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
