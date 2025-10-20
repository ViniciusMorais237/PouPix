using System.Data;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Repositories;
using API.Services;
using Microsoft.Data.SqlClient;

namespace API.Config

{
    public static class DIConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<IDbConnection>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var connectionString = config.GetConnectionString("DockerConnection");
                return new SqlConnection(connectionString);
            });

            services.AddScoped<IInfoBankService, InfoBankService>();
            services.AddScoped<IInfoBankRepository, InfoBankRepository>();
            services.AddScoped<IComprasService, ComprasService>();
            services.AddScoped<IComprasRepository, ComprasRepository>();
            services.AddScoped<IAnotacoesService, AnotacoesService>();
            services.AddScoped<IAnotacoesRepository, AnotacoesRepository>();

            return services;
        }
    }
}