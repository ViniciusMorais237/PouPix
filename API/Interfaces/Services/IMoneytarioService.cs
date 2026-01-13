using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces.Services
{
    public interface IMoneytarioService
    {
        Task<IEnumerable<InfoMoneyDiario>> CalcularMonetarioDiario(DateTime data, DateTime dataProximoPagamento, int valorEntrada, int idBanco);
        Task<int> CalcularLimiteFixo(DateTime dataEntrada, DateTime dataProximoPagamento, int valorEntrada);
        Task<InfoMoneyMes> ObterInfoMes(DateTime data);
        Task<bool> InserirInfoDiariaPadrao(DateTime data, DateTime dataProximoPagamento, int valor);
    }
}