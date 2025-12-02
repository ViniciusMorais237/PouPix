using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class InfoMoneyDiario
    {
        public DateTime Data { get; set; }
        public int Gasto { get; set; }
        public int LimiteDinamico { get; set; }
        public int LimiteFixo { get; set; }
    }
}