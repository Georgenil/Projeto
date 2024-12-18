using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;

namespace Projeto.Service.Interfaces
{
    public interface IUserService
    {
        Task<Response> Register(User user);
    }
}
