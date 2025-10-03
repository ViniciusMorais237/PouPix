using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Dto
{
    public class CompraInsertDTO
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int IdCategoria { get; set; }
        public string? Categoria { get; set; } = "NAO";
        public DateTime Data { get; set; } = DateTime.Now;
        public string? Comentario { get; set; }
    }
}