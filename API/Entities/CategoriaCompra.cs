using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public record CategoriaCompra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}