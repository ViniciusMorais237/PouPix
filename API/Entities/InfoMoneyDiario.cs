using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class InfoMoneyDiario
    {
        public DateTime Data { get; set; }
        public int LimiteDiario { get; set; }
        public int TotalGasto { get; set; }
    }
}