using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces.Services;
using API.Interfaces.Repositories;
using API.Entities;

namespace API.Services
{
    public class MoneytarioService : IMoneytarioService
    {
        private readonly IMoneytarioRepository _moneytarioRepository;

        public MoneytarioService(IMoneytarioRepository moneytarioRepository)
        {
            _moneytarioRepository = moneytarioRepository;
        }

        public async Task<double> CalcularLimiteFixo(DateTime data)
        {
            var infoMes = await _moneytarioRepository.ObterInfoMes(data);
            int dias = CalcularDiasEntrePagamentos(infoMes.DataPagamento, infoMes.DataProximoPagamento);
            var valorReserva = infoMes.Entrada * infoMes.PorcentagemInvestimento / 100;
            var valorRestante = infoMes.Entrada - valorReserva;
            return valorRestante / dias;
        }

        public async Task<InfoMoneyMes> ObterInfoMes(DateTime data)
        {
            var dataMap = data;
            return await _moneytarioRepository.ObterInfoMes(dataMap);
        }

        public int CalcularDiasEntrePagamentos(DateTime ultimoPagamento, DateTime proximoPagamento)
        {
            if (proximoPagamento == DateTime.MinValue)
                return (ultimoPagamento.AddMonths(1) - ultimoPagamento).Days;

            return (proximoPagamento - ultimoPagamento).Days;
        }
    }
}