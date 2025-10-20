using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class MonetarioDiario
    {
        public int Dia { get; set; }
        public decimal Saldo { get; set; }
        public decimal Limite { get; set; }
    }
}