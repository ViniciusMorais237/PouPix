using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Dto
{
    public class CompraInsertDTO
    {
        public int IdBanco { get; set; }
        public int IdCategoria { get; set; }
        public int Saldo { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int? Prestacao { get; set; } = null;
        public DateTime Data { get; set; } = DateTime.Now;
    }
}