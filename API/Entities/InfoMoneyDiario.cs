using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class InfoMoneyDiario
    {
        public InfoMoneyDiario()
        {
            CalcularSobra();
        }
        public DateTime Data { get; set; }
        public int Gasto { get; set; }
        public int LimiteDinamico { get; set; }
        public int LimiteFixo { get; set; }
        public int? Sobra { get; set; } = null;
        public int PoupadoAtual { get; set; }

        private void CalcularSobra()
        {
            if (Data.Date <= DateTime.Now.Date)
                this.Sobra = LimiteFixo - Gasto;
        }
    }
}