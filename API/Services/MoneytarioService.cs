using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces.Services;
using API.Interfaces.Repositories;
using API.Entities;
using System.Data.Common;

namespace API.Services
{
    public class MoneytarioService : IMoneytarioService
    {
        private readonly IMoneytarioRepository _moneytarioRepository;

        public MoneytarioService(IMoneytarioRepository moneytarioRepository)
        {
            _moneytarioRepository = moneytarioRepository;
        }

        public async Task<int> CalcularLimiteFixo(DateTime data)
        {
            //var infoMes = await _moneytarioRepository.ObterInfoMes(data);
            DateTime dataPagamento = new DateTime(2025, 11, 05);
            var infoMes = new InfoMoneyMes()
            {
                DataPagamento = dataPagamento,
                DataProximoPagamento = dataPagamento.AddMonths(1),
                Entrada = 2200,
                PorcentagemInvestimento = 50
            };
            int dias = CalcularDiasEntrePagamentos(infoMes.DataPagamento, infoMes.DataProximoPagamento);
            var valorReserva = infoMes.Entrada * infoMes.PorcentagemInvestimento / 100;
            var valorRestante = infoMes.Entrada - valorReserva;
            return (int)(valorRestante / dias);
        }

        public async Task<int> CalcularLimiteFixo(DateTime data, int saldoAtual)
        {
            //var infoMes = await _moneytarioRepository.ObterInfoMes(data);
            DateTime dataPagamento = new DateTime(2025, 11, 05);
            var infoMes = new InfoMoneyMes()
            {
                DataPagamento = dataPagamento,
                DataProximoPagamento = dataPagamento.AddMonths(1),
                Entrada = 2200,
                PorcentagemInvestimento = 50
            };
            int dias = CalcularDiasEntrePagamentos(data, infoMes.DataProximoPagamento);
            var valorRestante = saldoAtual;
            return (int)(valorRestante / dias);
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

        public async Task<IEnumerable<InfoMoneyDiario>> CalcularMonetarioDiario(DateTime data)
        {
            int limiteDiario = await CalcularLimiteFixo(new DateTime(2025, 11, 24));

            DateTime dataPagamento = new DateTime(2025, 11, 05);
            var infoMes = new InfoMoneyMes()
            {
                DataPagamento = dataPagamento,
                DataProximoPagamento = dataPagamento.AddMonths(1),
                Entrada = 2200,
                PorcentagemInvestimento = 50
            };

            var dias = GerarListaDiasMock(infoMes.DataPagamento, infoMes.DataProximoPagamento);
            var primeiroDia = dias.First();
            primeiroDia.LimiteDiario = limiteDiario;
            int poupado = (int)(primeiroDia.LimiteDiario - primeiroDia.TotalGasto);

            if (poupado < 0)
            {
                limiteDiario = await CalcularLimiteFixo(primeiroDia.Data.AddDays(1), 2200 - (int)primeiroDia.TotalGasto);
                var segundoDia = dias.Where(d => d.Data == primeiroDia.Data.AddDays(1));
            }

                foreach (var dia in dias)
                {
                    var sobra = primeiroDia.TotalGasto;

                }

            throw new NotImplementedException();
        }

        private List<InfoMoneyDiario> GerarListaDiasMock(DateTime dataInicio, DateTime dataFim)
        {
            List<InfoMoneyDiario> datas = new();

            var random = new Random();

            for (DateTime data = dataInicio; data.CompareTo(dataFim) < 0; data.AddDays(1))
            {
                datas.Add(new InfoMoneyDiario
                {
                    Data = data,

                });
            }

            return datas;
        }
    }
}