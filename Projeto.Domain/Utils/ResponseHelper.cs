using Microsoft.AspNetCore.Mvc;
using Projeto.Domain.ViewModels;

namespace Projeto.Domain.Utils
{
    public class ResponseHelper : ControllerBase
    {
        public IActionResult CreateResponse(Response response)
        {
            return response.StatusCode switch
            {
                200 => Ok(response),
                500 => StatusCode(500, response),
                422 => UnprocessableEntity(response),
                409 => Conflict(response),
                401 => Unauthorized(response),
                404 => NotFound(response),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
