using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Infra.Utils.ExtensionMethod;

namespace Projeto.Facade.Interfaces
{
    public interface IUserFacade
    {
        Task<Response<UserViewModel>> Register(UserViewModel user);
    }
}
