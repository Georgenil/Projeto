using Microsoft.AspNetCore.Mvc;
using Projeto.Application.Interfaces;
using Projeto.Domain.Utils;
using Projeto.Domain.ViewModels;
using Projeto.Models;

namespace Projeto.Controllers
{
    [Route("colaborador")]
    [ApiController]
    public class ColaboradorController
    {
        private readonly IColaboradorService _colaboradorService;
        public ColaboradorController(IColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }


        /// <summary>
        /// Obter todos os colaboradores cadastrados no banco de dados
        /// </summary>
        /// <returns>Lista de colaboradores</returns>
        [HttpGet("listar")]
        public async Task<IActionResult> BuscarTodos()
        {
            return new ResponseHelper().CreateResponse(await _colaboradorService.BuscarTodos());
        }


        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar(ColaboradorViewModel model)
        {
            return new ResponseHelper().CreateResponse(await _colaboradorService.Cadastrar(model));
        }

        [HttpPost("editar")]
        public async Task<IActionResult> Editar([FromBody] ColaboradorViewModel model)
        {
            return new ResponseHelper().CreateResponse(await _colaboradorService.Editar(model));
        }

        [HttpPost("remover")]
        public async Task<IActionResult> Remover([FromBody] int id)
        {
            return new ResponseHelper().CreateResponse(await _colaboradorService.Remover(id));
        }
    }
}
