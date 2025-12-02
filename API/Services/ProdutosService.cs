using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Produto;
using API.Interfaces.Services;

namespace API.Services
{
    public class ProdutosService : IProdutosService
    {
        private readonly AmazonScraping _amazon;
        public ProdutosService()
        {
            _amazon = new AmazonScraping();
        }
        public async Task<List<string>> AdicionarProduto(ProdutoScrapingDto produto)
        {
            return await _amazon.RetornarProdutosPorPesquisa(produto.Nome);
        }

        public async Task<byte[]> AdicionarUrlScraping(Uri uri)
        {
            var downloads = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "downloads");

            var caminho = Path.Combine(downloads, "urlSites.csv");

            if(!File.Exists(caminho))
            {
                File.WriteAllLines(caminho, new [] {"URL"});
            }

            File.AppendAllLines(caminho, new[] {uri.ToString()});

            return await File.ReadAllBytesAsync(caminho);
        }
    }
}