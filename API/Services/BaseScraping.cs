using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace API.Services
{
    public abstract class BaseScraping : IAsyncDisposable
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        protected async Task<IPage> ObterPagina()
        {
            if(_playwright == null)
            {
                _playwright = await Playwright.CreateAsync();

                _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = false
                });
            }
            return await _browser!.NewPageAsync();
        }

        protected async Task<List<string>> ObterInfoTodosPorSeletor(IPage pagina)
        {
            var preco = await pagina.WaitForSelectorAsync("span.a-price-whole");
            var nome = await pagina.WaitForSelectorAsync("class.a-size-base-plus a-spacing-none a-color-base a-text-normal");
            
            var elementos = await pagina.QuerySelectorAllAsync("span.a-price-whole");
            var nomes = await pagina.QuerySelectorAllAsync("class.a-size-base-plus a-spacing-none a-color-base a-text-normal");
            List<string> lista = new();
            int contador = 0;
            foreach (var item in elementos)
            {
                var preco1 = await item.InnerTextAsync();
                var nome1 = await nomes[contador].InnerTextAsync();
                lista.Add($"{preco1} : {nome1}");
            }
            return lista;
        }

        public async ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}