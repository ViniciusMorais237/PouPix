using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnotacoesController : ControllerBase
    {
        private readonly IAnotacoesService _service;

        public AnotacoesController(IAnotacoesService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(AnotacaoResponseDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterAnotacoesPorDia(DateTime date)
        {
            return Ok(await _service.ObterAnotacoesPorDia(date));
        }

        [HttpPost]
        public async Task<IActionResult> Anotar([FromForm] AnotacaoCreateDTO anotacao)
        {
            return Ok(await _service.Anotar(anotacao));
        }
    }
}