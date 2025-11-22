using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class InfoMoneyMes
    {
        public double Entrada { get; set; }
        public int PorcentagemInvestimento { get; set; }
        public double LimiteFixo { get; set; }
        public double Poupado { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataProximoPagamento { get; set; }
    }
}