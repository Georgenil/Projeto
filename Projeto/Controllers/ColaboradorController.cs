using Microsoft.AspNetCore.Mvc;
using Projeto.Facade;
using Projeto.Infra.Utils.ExtensionMethod;

namespace Projeto.Controllers
{
    [Route("colaborador")]
    [ApiController]
    public class ColaboradorController :ControllerBase
    {
        private readonly IColaboradorFacade _colaboradorFacade;
        public ColaboradorController(IColaboradorFacade colaboradorFacade)
        {
            _colaboradorFacade = colaboradorFacade;
        }


        /// <summary>
        /// Obter todos os colaboradores cadastrados no banco de dados
        /// </summary>
        /// <returns>Lista de colaboradores</returns>
        [HttpGet("listar")]
        public IActionResult BuscarTodos()
        {
            return  _colaboradorFacade.BuscarTodos().GetResult();
        }


        //[HttpPost("cadastrar")]
        //public async Task<IActionResult> Cadastrar(ColaboradorViewModel model)
        //{
        //    return new ResponseHelper().CreateResponse(await _colaboradorService.Cadastrar(model));
        //}

        //[HttpPost("editar")]
        //public async Task<IActionResult> Editar([FromBody] ColaboradorViewModel model)
        //{
        //    return new ResponseHelper().CreateResponse(await _colaboradorService.Editar(model));
        //}

        //[HttpPost("remover")]
        //public async Task<IActionResult> Remover([FromBody] int id)
        //{
        //    return new ResponseHelper().CreateResponse(await _colaboradorService.Remover(id));
        //}
    }
}
