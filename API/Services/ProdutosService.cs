using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.Dto;
using API.Entities.Produto;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using API.Repositories.Models;

namespace API.Services
{
    public class ProdutosService : IProdutosService
    {
        private readonly AmazonScraping _amazon;
        private readonly IProdutosRepository _repository;
        public ProdutosService(IProdutosRepository repository)
        {
            _amazon = new AmazonScraping();
            _repository = repository;
        }

        public async Task<bool> AdicionarDesejo(DesejoInsertDto desejo)
        {
            var caminhoImagem = desejo.Imagem == null ? string.Empty : ImagemService.CriarArquivoDeImagem(desejo.Imagem);

            var desejoDb = new DesejoDB() { Nome = desejo.Nome, Valor = desejo.Valor, CaminhoImagem = caminhoImagem };

            return await _repository.AdicionarDesejo(desejoDb);
        }

        public async Task<List<string>> AdicionarProduto(ProdutoScrapingDto produto)
        {
            return await _amazon.RetornarProdutosPorPesquisa(produto.Nome);
        }

        public async Task<byte[]> AdicionarUrlScraping(Uri uri)
        {
            var downloads = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "downloads");

            var caminho = Path.Combine(downloads, "urlSites.csv");

            if (!File.Exists(caminho))
            {
                File.WriteAllLines(caminho, new[] { "URL" });
            }

            File.AppendAllLines(caminho, new[] { uri.ToString() });

            return await File.ReadAllBytesAsync(caminho);
        }

        public async Task<bool> InserirQuantiaDesejo(int id, int quantia)
        {
            return await _repository.InserirQuantiaDesejo(id, quantia);
        }

        public async Task<IEnumerable<DesejoDB>> ObterDesejos()
        {
            return await _repository.ObterDesejos();
        }

        public Task<bool> RealizarDesejo(int id)
        {
            throw new NotImplementedException();
        }
    }
}