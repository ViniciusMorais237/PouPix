using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<IActionResult> InfoMes(DateTime data)
        {
            return Ok(await _moneytarioService.ObterInfoMes(data));
        }
    }
}