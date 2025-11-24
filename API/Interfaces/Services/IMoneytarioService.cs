using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces.Services
{
    public interface IMoneytarioService
    {
        Task<IEnumerable<InfoMoneyDiario>> CalcularMonetarioDiario(DateTime data);
        Task<int> CalcularLimiteFixo(DateTime data);
        Task<InfoMoneyMes> ObterInfoMes(DateTime data);
    }
}