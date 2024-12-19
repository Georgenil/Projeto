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
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserDTO, UserViewModel>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<UserViewModel, UserDTO>()
                .ForMember(d => d.user, opt => opt.MapFrom(s => s)).ReverseMap();
            CreateMap<User, UserLoginViewModel>().ReverseMap();

            CreateMap<Colaborador, ColaboradorViewModel>().ReverseMap();
        }
    }
}
