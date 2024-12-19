using Projeto.Facade;
using Projeto.Facade.Facades;
using Projeto.Facade.Interfaces;
using Projeto.Infra.Data;
using Projeto.Infra.Data.Repositories;
using Projeto.Service;

namespace Projeto.Configurations
{
    public static class ServicesConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<IColaboradorFacade, ColaboradorFacade>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioFacade, UsuarioFacade>();
        }
    }
}
