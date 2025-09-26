using Dapper;
using API.Entities;

namespace API.Interfaces.Repositories
{
    public interface IPaginaInicialRepository
    {
        Task<IndicadoresPaginaInicial> ObterIndicadoresPaginaInicial();
        Task<bool> EditarIndicadoresPaginaInicial(string query,DynamicParameters param);
    }
}