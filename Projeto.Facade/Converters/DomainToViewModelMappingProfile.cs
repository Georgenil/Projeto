using AutoMapper;
using Projeto.Domain.Models;
using Projeto.Facade.ViewModels;

namespace Projeto.Facade.Converters
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<Colaborador, ColaboradorViewModel>().ReverseMap();
        }
    }
}
