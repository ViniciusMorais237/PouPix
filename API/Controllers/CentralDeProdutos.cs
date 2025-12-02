using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Produto;
using API.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;

namespace API.Controllers
{
    public class CentralDeProdutos : BaseController
    {
        private readonly IProdutosService _produtos;

        public CentralDeProdutos(IProdutosService produtos)
        {
            _produtos = produtos;
        }

        [HttpPost("adicionar-produtos")]
        public async Task<IActionResult> AdicionarProduto(ProdutoScrapingDto produto)
        {
            return Ok(await _produtos.AdicionarProduto(produto));
        }

        [HttpPost("urls")]
        public async Task<IActionResult> AdicionarUrlScraping(Uri uri, string nomeArquivo)
        {
            return File(await _produtos.AdicionarUrlScraping(uri),"text/csv",nomeArquivo);
        }
    }
}