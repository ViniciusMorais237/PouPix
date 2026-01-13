using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Produto;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using API.Entities.Dto;

namespace API.Controllers
{
    public class CentralDeProdutos : BaseController
    {
        private readonly IProdutosService _produtos;

        public CentralDeProdutos(IProdutosService produtos)
        {
            _produtos = produtos;
        }

        [HttpPost("desejos")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AdicionarDesejo(DesejoInsertDto desejo)
        {
            return Ok(await _produtos.AdicionarDesejo(desejo));
        }

        [HttpPatch("desejos/realizarDesejo{id}")]
        public async Task<IActionResult> RealizarDesejo(int id)
        {
            return Ok(await _produtos.RealizarDesejo(id));
        }

        [HttpGet("desejos")]
        public async Task<IActionResult> ObterDesejos()
        {
            return Ok(await _produtos.ObterDesejos());
        }

        [HttpPatch("desejos/inserirQuantia{id}")]
        public async Task<IActionResult> InserirQuantiaDesejo(int id, int quantia)
        {
            return Ok(await _produtos.InserirQuantiaDesejo(id, quantia));
        }

        [HttpPost("adicionar-produtos")]
        public async Task<IActionResult> AdicionarProduto(ProdutoScrapingDto produto)
        {
            return Ok(await _produtos.AdicionarProduto(produto));
        }

        [HttpPost("urls")]
        public async Task<IActionResult> AdicionarUrlScraping(Uri uri, string nomeArquivo)
        {
            return File(await _produtos.AdicionarUrlScraping(uri), "text/csv", nomeArquivo);
        }
    }
}