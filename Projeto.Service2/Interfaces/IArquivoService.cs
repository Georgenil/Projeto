using Microsoft.AspNetCore.Http;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.ExtensionMethod;

namespace Projeto.Service.Interfaces
{
    public interface IArquivoService
    {
        Task<Response<bool>> Uploads(ICollection<IFormFile> files);
    }
}
