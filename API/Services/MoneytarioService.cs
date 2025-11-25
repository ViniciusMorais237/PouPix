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
            var infoMes = await _moneytarioRepository.ObterInfoMes(data);
            int dias = CalcularDiasEntrePagamentos(infoMes.DataPagamento, infoMes.DataProximoPagamento);
            var valorReserva = infoMes.Entrada * infoMes.PorcentagemInvestimento / 100;
            var valorRestante = infoMes.Entrada - valorReserva;
            return (int)(valorRestante / dias);
        }

        public async Task<int> CalcularLimiteFixo(DateTime data, int saldoAtual)
        {
            var infoMes = await _moneytarioRepository.ObterInfoMes(data);
            int dias = CalcularDiasEntrePagamentos(data, infoMes.DataProximoPagamento);
            var valorRestante = saldoAtual;
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

        public async Task<IEnumerable<InfoMoneyDiario>> CalcularMonetarioDiario(DateTime data)
        {
            int limiteMensalDiario = await CalcularLimiteFixo(data);
            int saldoAtual = 2751;

            var dias = GerarListaDiasMock(data, data.AddMonths(1));
            var historico = new List<(DateTime data, int Saldo)>();

            int poupadoTotal = 0;
            int dividaAcumulada = 0;

            foreach (var dia in dias)
            {
                var limiteBase = limiteMensalDiario;
                dia.LimiteDiario = limiteBase + poupadoTotal;

                int sobraDiaria = limiteBase - dia.TotalGasto;

                saldoAtual = saldoAtual - dia.TotalGasto;

                historico.Add(new(dia.Data, saldoAtual));

                if (sobraDiaria < 0)
                {
                    dividaAcumulada += sobraDiaria;

                    var proximoDia = dia.Data.AddDays(1);
                    limiteMensalDiario = await CalcularLimiteFixo(proximoDia, historico.Where(h => h.data == dia.Data).Select(h => h.Saldo).First());

                    poupadoTotal = 0;
                }
                else
                {
                    if (dividaAcumulada < 0)
                    {
                        int pagamento = Math.Min(sobraDiaria, Math.Abs(dividaAcumulada));
                        dividaAcumulada += pagamento;
                        sobraDiaria -= pagamento;
                    }

                    poupadoTotal += sobraDiaria;

                    if(poupadoTotal < 0) poupadoTotal = 0;
                }
            }

            return dias;
        }

        private List<InfoMoneyDiario> GerarListaDiasMock(DateTime dataInicio, DateTime dataFim)
        {
            List<InfoMoneyDiario> datas = new();

            var random = new Random();

            var data = dataInicio;

            while (data < dataFim)
            {
                datas.Add(new InfoMoneyDiario
                {
                    Data = data,
                    TotalGasto = random.Next(45, 120)
                });

                data = data.AddDays(1);
            }

            return datas;
        }
    }
}