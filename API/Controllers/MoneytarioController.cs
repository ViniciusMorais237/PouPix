using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MoneytarioController : BaseController
    {
        private readonly IMoneytarioService _moneytarioService;
        public MoneytarioController(IMoneytarioService moneytarioService)
        {
            _moneytarioService = moneytarioService;
        }

        [HttpGet("calculo-teto")]
        public async Task<IActionResult> CalcularTeto(DateTime data)
        {
            return Ok(await _moneytarioService.CalcularLimiteFixo(data));
        }

        [HttpPost("postar-info-diaria")]
        public async Task<IActionResult> PostarInfoDiaria(DateTime data)
        {
            return Ok(await _moneytarioService.InserirInfoDiariaPadrao(data));
        }

        [HttpGet("calculo-monetario-diario")]
        // [ProducesResponseType(typeof(InfoMoneyDiario), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalcularMonetariDiario(DateTime data, int idBanco)
        {
            return Ok(await _moneytarioService.CalcularMonetarioDiario(data, idBanco));
        }

        [HttpGet]
        public async Task<IActionResult> InfoMes(DateTime data)
        {
            return Ok(await _moneytarioService.ObterInfoMes(data));
        }
        // [HttpPost("inserir-info-mes")]
        // public async Task<IActionResult> InserirInfoMes()
        // {
        //     return Ok(await _moneytarioService.InserirInfoMes());
        // }
    }
}