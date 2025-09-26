using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaginaInicialController : ControllerBase
    {
        private readonly IPaginaInicialService _paginaInicialService;

        public PaginaInicialController(IPaginaInicialService paginaInicialService)
        {
            _paginaInicialService = paginaInicialService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IndicadoresPaginaInicial), StatusCodes.Status200OK)]
        public async Task<ActionResult<IndicadoresPaginaInicial>> ObterIndicadoresPaginaInicial()
        {
            return Ok(await _paginaInicialService.ObterIndicadoresPaginaInicial());
        }

        [HttpPatch]
        [ProducesResponseType(typeof(IndicadoresPaginaInicial), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> EditarIndicadoresPaginaInicial(EdicaoIndicadoresDto edicaoIndicadores)
        {
            return Ok(await _paginaInicialService.EditarIndicadoresPaginaInicial(edicaoIndicadores));
        }
    }
}
