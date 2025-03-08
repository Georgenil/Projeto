using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.Models;
using Projeto.Facade;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service.Interfaces;

namespace Projeto.Controllers
{
    [Route("Arquivo")]
    [ApiController]
    public class ArquivoController : ControllerBase
    {
        private readonly IArquivoService _arquivoService;

        public ArquivoController(IArquivoService arquivoService)
        {
            _arquivoService = arquivoService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ICollection<IFormFile> files)
        {
            
            return await _arquivoService.Uploads(files).GetAsyncResult();
        }

        //[HttpPost("urlFileDownload")]
        //public async void UrlFileDownloadAsync(string url)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        var data = await httpClient.GetByteArrayAsync(url);
        //        File.WriteAllBytes("Exemplo.dat", data);
        //    }
            
        //}
    }
}
