using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Produto
{
    public class ProdutoScrapingDto
    {
        public string Nome { get; set; } = string.Empty;
        public List<string> Urls { get; set; } = [];
    }
}