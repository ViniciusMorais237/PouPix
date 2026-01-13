using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Services;

namespace API.Controllers
{
    public class ComprasController : BaseController
    {
        private readonly IComprasService _comprasService;

        public ComprasController(IComprasService comprasService)
        {
            _comprasService = comprasService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CompraInsertDTO), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCompra(CompraInsertDTO compra)
        {
            return Ok(await _comprasService.PostCompra(compra));
        }

        [HttpGet("{data}")]
        [ProducesResponseType(typeof(HistoricoCompra), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHistorico(DateTime data)
        {
            return Ok(await _comprasService.GetHistoricoCompras(data.ToString()));
        }


        [HttpPost("categorias")]
        [ProducesResponseType(typeof(CategoriaCompra), StatusCodes.Status201Created)]
        public async Task<IActionResult> PostCategoria(string nome)
        {
            return Ok(await _comprasService.PostCategoria(nome));
        }

        [HttpGet("categorias")]
        [ProducesResponseType(typeof(CategoriaCompra), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategorias()
        {
            return Ok(await _comprasService.GetCategorias());
        }

    }
}