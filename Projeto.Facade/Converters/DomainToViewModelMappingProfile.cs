using AutoMapper;
using Projeto.Domain.Models;
using Projeto.Domain.ViewModels;
using Projeto.Service.DTO;

namespace Projeto.Facade.Converters
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioCadastroViewModel>().ReverseMap();
            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginViewModel>().ReverseMap();
            CreateMap<UsuarioDTO, UsuarioViewModel>().ReverseMap();
            CreateMap<UsuarioViewModel, UsuarioDTO>()
                .ForMember(d => d.usuario, opt => opt.MapFrom(s => s)).ReverseMap();

            CreateMap<Colaborador, ColaboradorViewModel>().ReverseMap();
        }
    }
}
