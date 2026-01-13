using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Dto;
using API.Entities.Produto;
using API.Repositories.Models;

namespace API.Interfaces.Services
{
    public interface IProdutosService
    {
        Task<bool> AdicionarDesejo(DesejoInsertDto produto);
        Task<bool> RealizarDesejo(int id);
        Task<IEnumerable<DesejoDB>> ObterDesejos();
        Task<bool> InserirQuantiaDesejo(int id, int quantia);
        Task<List<string>> AdicionarProduto(ProdutoScrapingDto produto);
        Task<byte[]> AdicionarUrlScraping(Uri uri);
    }
}