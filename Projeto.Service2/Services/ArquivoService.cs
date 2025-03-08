using Microsoft.AspNetCore.Http;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Data.Interfaces;
using Projeto.Infra.Utils.ExtensionMethod;
using Projeto.Service.Interfaces;
using System.Net;

namespace Projeto.Service.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IArquivoRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ArquivoService(IArquivoRepository arquivoRepository, IUnitOfWork unitOfWork)
        {
            _repository = arquivoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<bool>> Uploads(ICollection<IFormFile> files)
        {
            try
            {
                if (files == null || files.Count == 0)
                    return new Response<bool>(HttpStatusCode.NotAcceptable, "Arquivos nulos.", false);

                IList<Arquivo> arquivos = new List<Arquivo>();

                foreach (IFormFile formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await formFile.CopyToAsync(stream);

                            var arquivo = new Arquivo();

                            arquivo.Bytes = stream.ToArray();
                            arquivo.Nome = formFile.FileName;
                            arquivo.Extensao = Path.GetExtension(formFile.FileName);

                            arquivos.Add(arquivo);
                        }
                    }
                }

                _repository.AddRange(arquivos);
                await _unitOfWork.CommitAsync();

                return new Response<bool>()
                {
                    Entity = true,
                    Status = HttpStatusCode.OK,
                };

            }
            catch (Exception ex)
            {
                return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
