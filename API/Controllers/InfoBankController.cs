using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Services;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoBankController : ControllerBase
    {
        private readonly IInfoBankService _InfoBankService;

        public InfoBankController(IInfoBankService InfoBankService)
        {
            _InfoBankService = InfoBankService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(InfoBank), StatusCodes.Status200OK)]
        public async Task<ActionResult<InfoBank>> ObterInfoBank()
        {
            return Ok(await _InfoBankService.ObterInfoBank());
        }

        [HttpGet("Informacoes-monetarias-diarias")]
        [ProducesResponseType(typeof(MonetarioDiario), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MonetarioDiario>>> ObterInfoMoney()
        {
            return Ok(await _InfoBankService.ObterInfoMoney());
        }

        [HttpPatch]
        [ProducesResponseType(typeof(InfoBank), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> EditarInfoBank(EdicaoIndicadoresDto edicaoInfoBank)
        {
            return Ok(await _InfoBankService.EditarInfoBank(edicaoInfoBank));
        }
    }
}
