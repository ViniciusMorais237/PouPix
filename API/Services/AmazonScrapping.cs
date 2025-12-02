using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces.Services;
using Microsoft.Playwright;

namespace API.Services
{
    public class AmazonScraping : BaseScraping, IScraping
    {
        public async Task<List<string>> RetornarProdutosPorPesquisa(string pesquisa)
        {
            string query = pesquisa.Replace(" ", "+");
            var url = $"https://www.amazon.com.br/s?k={query}";

            var page = await ObterPagina();
            await page.GotoAsync(url);

            try
            {
                var preco = await ObterInfoTodosPorSeletor(page);
                return preco;
                //var nome = await ObterInfoPorSeletor(page, "a-price-whole");
                //var imagem = await ObterInfoPorSeletor(page, "a-price-whole");
                //return double.Parse(preco.Replace(".", ""));
            }
            catch 
            {
                
                throw;
            }
        }
    }
}