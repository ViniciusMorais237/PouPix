using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Produto;

namespace API.Interfaces.Services
{
    public interface IProdutosService
    {
        Task<List<string>> AdicionarProduto(ProdutoScrapingDto produto);
        Task<byte[]> AdicionarUrlScraping(Uri uri);
    }
}