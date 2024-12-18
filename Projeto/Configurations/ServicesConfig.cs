using Projeto.Application.Interfaces;
using Projeto.Domain.DataInterfaces;
using Projeto.Infra.Data.Repositories;
using Projeto.Service.DataInterfaces;
using Projeto.Service.Interfaces;
using Projeto.Service.Services;

namespace Projeto.Configurations
{
    public static class ServicesConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
