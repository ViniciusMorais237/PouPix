using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Historico
    {
        public int Id { get; set; }
        public int IdBanco { get; set; }
        public int IdCategoria { get; set; }
        public int Saldo { get; set; }
        public int Valor { get; set; }
        public int Prestacao { get; set; }
        public DateTime Data { get; set; }
    }
}