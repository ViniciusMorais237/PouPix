using API.Entities;
using API.Entities.Dto;

namespace API.Interfaces.Services
{
    public interface IPaginaInicialService
    {
        Task<IndicadoresPaginaInicial> ObterIndicadoresPaginaInicial();
        Task<bool> EditarIndicadoresPaginaInicial(EdicaoIndicadoresDto edicaoIndicadores);
    }
}