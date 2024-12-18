using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Projeto.Facade.Converters
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.AddProfile<DomainToViewModelMappingProfile>();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

    }
}
