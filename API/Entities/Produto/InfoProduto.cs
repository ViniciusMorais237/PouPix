using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Produto
{
    public class InfoProduto
    {
        public string Nome { get; set; } = string.Empty;
        public double Valor { get; set; }
        public string Imagem { get; set; } = string.Empty;
    }
}