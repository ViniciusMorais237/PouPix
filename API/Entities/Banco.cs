using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Banco
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Saldo { get; set; }
    }
}