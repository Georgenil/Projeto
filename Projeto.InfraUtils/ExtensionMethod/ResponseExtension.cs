using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Infra.Utils.ExtensionMethod
{
    public static class ResponseExtension
    {
        public static IActionResult GetResultImage(this Response<MemoryStream> response)
        {
            if (response.Status == HttpStatusCode.OK)
            {
                return new FileStreamResult(response.Entity, "image/jpeg");
            }

            return GetResult<MemoryStream>(response);
        }

        public static async Task<IActionResult> GetAsyncResult<TEntity>(this Task<Response<TEntity>> response)
        {
            return GetResult(await response);
        }
        public static async Task<IActionResult> GetAsyncResult<TEntity>(this Task<PagedResponse<TEntity>> response)
        {
            return GetResult(await response);
        }

        public static IActionResult GetResult<TEntity>(this Response<TEntity> response)
        {
            string responseValue = string.Empty;

            if (response.Messages != null && response.Messages.Length > 0)
                responseValue = string.Concat(string.Join(" \n ", response.Messages));

            if (response.Entity != null && response.Entity.GetType() == typeof(string))
                responseValue = response.Entity.ToString();

            //TODO: Implementar tradução das mensagens com o método em StringExtension

            return new ObjectResult(response)
            {
                StatusCode = (int)response.Status
            };
        }

        public static IActionResult GetResultApi<TEntity>(this Response<TEntity> response)
        {
            string responseValue = string.Empty;

            if (response.Messages != null && response.Messages.Length > 0)
                responseValue = string.Concat(string.Join(" \n ", response.Messages));

            if (response.Entity != null && response.Entity.GetType() == typeof(string))
                responseValue = response.Entity.ToString();

            //TODO: Implementar tradução das mensagens com o método em StringExtension

            return new ObjectResult(response)
            {
                StatusCode = (int)response.Status
            };
        }
    }
}
