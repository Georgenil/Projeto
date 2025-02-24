using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Models;

namespace Projeto.Controllers
{
    [Route("file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<ActionResult> Upload([FromForm] ICollection<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest();

            List<Arquivo> arquivos = new();

            foreach (IFormFile formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);

                        var arquivo = new Arquivo();

                        arquivo.Bytes = stream.ToArray();
                        arquivo.Name = formFile.FileName;
                        arquivo.Extensao = Path.GetExtension(formFile.Name);

                        arquivos.Add(arquivo);
                    }
                }
            }

            //chamar repositório para persistir

            return Ok();
        }
    }
}
