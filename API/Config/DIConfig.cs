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
                var connectionString = config.GetConnectionString("DefaultConnection");
                return new SqlConnection(connectionString);
            });
            services.AddScoped<IComprasService, ComprasService>();
            services.AddScoped<IComprasRepository, ComprasRepository>();
            services.AddScoped<IAnotacoesService, AnotacoesService>();
            services.AddScoped<IAnotacoesRepository, AnotacoesRepository>();

            services.AddScoped<IMoneytarioService, MoneytarioService>();
            services.AddScoped<IMoneytarioRepository, MoneytarioRepository>();

            services.AddScoped<IProdutosService, ProdutosService>();
            services.AddScoped<IProdutosRepository, ProdutosRepository>();



            return services;
        }
    }
}