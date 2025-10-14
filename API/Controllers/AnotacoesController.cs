using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
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

        [HttpPost]
        public async Task<IActionResult> Anotar([FromForm] Anotacao anotacao)
        {
            return Ok(await _service.Anotar(anotacao));
        }
    }
}