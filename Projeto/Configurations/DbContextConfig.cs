using Microsoft.EntityFrameworkCore;
using Projeto.Infra.Data;

namespace Projeto.Configurations
{
    public static class DbContextConfig
    {
        public static void AddDatabaseConfigurationLocal(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
