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

        public async Task<int> CalcularLimiteFixo(DateTime dataEntrada, DateTime dataProximoPagamento, int valorEntrada)
        {
            int dias = CalcularDiasEntrePagamentos(dataEntrada, dataProximoPagamento);
            return valorEntrada / dias;
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
            return await _moneytarioRepository.ObterInfoMes(data);
        }

        public async Task<List<InfoMoneyDiario>> ObterInfoDiario(DateTime data)
        {
            return (await _moneytarioRepository.ObterInfoDiariaMensal(data)).ToList();
        }

        public int CalcularDiasEntrePagamentos(DateTime ultimoPagamento, DateTime proximoPagamento)
        {
            if (proximoPagamento == DateTime.MinValue)
                return (ultimoPagamento.AddMonths(1) - ultimoPagamento).Days;

            return (proximoPagamento - ultimoPagamento).Days;
        }

        public async Task<IEnumerable<InfoMoneyDiario>> CalcularMonetarioDiario(DateTime data, DateTime dataProximoPagamento, int valorEntrada, int idBanco)
        {
            int limiteFixo = await CalcularLimiteFixo(data, dataProximoPagamento, valorEntrada);
            int limiteDinamico = limiteFixo;
            var historico = await _moneytarioRepository.ObterHistorico(data);
            var banco = await _moneytarioRepository.ObterInfoBanco(idBanco) ?? throw new Exception("banco inexistente");
            var saldo = banco.Saldo;

            var dias = GerarListaDiasMock(data, data.AddMonths(1), limiteFixo);
            var diasRetornar = new List<InfoMoneyDiario>();

            int poupadoTotal = 0;
            int dividaAcumulada = 0;

            foreach (var dia in dias)
            {
                var gasto = historico.Any(h => h?.Data.Date == dia.Data.Date) ? historico.Select(h => h!.Valor).Sum() : 0;
                diasRetornar.Add(new InfoMoneyDiario
                {
                    Data = dia.Data,
                    Gasto = gasto,
                    LimiteFixo = limiteFixo,
                    LimiteDinamico = limiteDinamico + poupadoTotal,
                    PoupadoAtual = poupadoTotal
                });

                limiteDinamico -= gasto;

                int sobraDiaria = limiteFixo - dia.Gasto;
                saldo -= dia.Gasto;

                banco.Saldo -= dia.Gasto;

                if (sobraDiaria < 0)
                {
                    dividaAcumulada += sobraDiaria;

                    var proximoDia = dia.Data.AddDays(1);
                    limiteFixo = await CalcularLimiteFixo(proximoDia, saldo);
                    limiteDinamico = limiteFixo;

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

                    if (poupadoTotal < 0) poupadoTotal = 0;
                }
            }

            return diasRetornar;
        }

        private List<InfoMoneyDiario> GerarListaDiasMock(DateTime dataInicio, DateTime dataFim, int limiteFixo)
        {
            List<InfoMoneyDiario> datas = new();

            var data = dataInicio;

            while (data < dataFim)
            {
                datas.Add(new InfoMoneyDiario
                {
                    Data = data,
                    Gasto = 0,
                    LimiteFixo = limiteFixo,
                    LimiteDinamico = limiteFixo
                });

                data = data.AddDays(1);
            }

            return datas;
        }

        public async Task<bool> InserirInfoDiariaPadrao(DateTime data, DateTime dataProximoPagamento, int valor)
        {
            var limiteFixo = await CalcularLimiteFixo(data, dataProximoPagamento, valor);
            var diasMock = GerarListaDiasMock(data, dataProximoPagamento, limiteFixo);
            return await _moneytarioRepository.InserirInfoDiariaPadrao(diasMock);
        }
    }
}