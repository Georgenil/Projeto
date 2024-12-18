using Projeto.Application.Interfaces;
using Projeto.Application.Services;
using Projeto.Domain.DataInterfaces;
using Projeto.Infra.Data.Repositories;
using Projeto.Service.DataInterfaces;

namespace Projeto.Configurations
{
    public static class ServicesConfig
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
        }
    }
}
