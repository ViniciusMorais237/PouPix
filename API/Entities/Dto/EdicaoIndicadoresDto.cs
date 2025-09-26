using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Dto
{
    public class EdicaoIndicadoresDto
    {
        public decimal? Saldo { get; set; }
        public decimal? Renda { get; set; }
        public decimal? LimiteCredito { get; set; }
        public decimal? LimiteCreditoTotal { get; set; }
        public DateTime? DiaPagamento { get; set; }
    }
}