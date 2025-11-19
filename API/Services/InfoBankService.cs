using System.Reflection;
using Dapper;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class InfoBankService : IInfoBankService
    {
        private readonly IInfoBankRepository _InfoBankRepository;
        protected string _tabela = "[GerenciadorDeDenhero].[dbo].[InfoBank]";
        private readonly Dictionary<string, string> mapeamentoBanco = new Dictionary<string, string>
        {
            {"Saldo", "NU_SALDO"},
            {"Renda", "NU_RENDA"},
            {"LimiteCredito", "NU_LIMITE_CREDITO"},
            {"LimiteCreditoTotal", "NU_LIMITE_CREDITO_TOTAL"},
            {"Fatura", "NU_FATURA"},
            {"DiaPagamento", "DATA_PAGAMENTO"}
        };
        public InfoBankService(IInfoBankRepository InfoBankRepository)
        {
            _InfoBankRepository = InfoBankRepository;

        }

        public async Task<InfoBank> ObterInfoBank()
        {
            return await _InfoBankRepository.ObterInfoBank();
        }

        public async Task<IEnumerable<MonetarioDiario>> ObterInfoMoney()
        {
            var infoBank = await _InfoBankRepository.ObterInfoBank();

            var LimiteDiario = infoBank.DiaPagamento.Day;

            throw new NotImplementedException();
        }

        public async Task<bool> EditarInfoBank(EdicaoIndicadoresDto edicaoIndicadores)
        {
            var camposEditados = RetornarCamposEditados(edicaoIndicadores);
            var (query, param) = MontarUpdateQuery(camposEditados, mapeamentoBanco);
            return await _InfoBankRepository.EditarInfoBank(query, param);

        }

        protected Dictionary<string, object> RetornarCamposEditados(object objetoEditado)
        {
            var resultado = new Dictionary<string, object>();

            if (objetoEditado == null)
                return resultado;

            var tipo = objetoEditado.GetType();

            foreach (var prop in tipo.GetProperties())
            {
                var valor = prop.GetValue(objetoEditado);
                if (valor != null)
                    resultado[prop.Name] = valor;
            }
            return resultado;
        }

        protected (string, DynamicParameters) MontarUpdateQuery(Dictionary<string, object> CamposEditados, Dictionary<string, string> mapeamentoBanco)
        {
            var sets = new List<string>();
            var param = new DynamicParameters();

            foreach (var campo in CamposEditados)
            {
                var propriedade = campo.Key;
                if (!mapeamentoBanco.TryGetValue(propriedade, out var colunaBanco))
                {
                    throw new Exception($"Não existe mapeamento para a propriedade {propriedade}");
                }

                sets.Add($"{colunaBanco} = @{propriedade}");
                param.Add($"@{propriedade}", campo.Value);
            }
            var chaveValor = string.Join(",", sets);
            var query = $"UPDATE {_tabela} SET {chaveValor}";
            return (query, param);
        }

        protected async Task<decimal> CalcularLimiteGastoDiario()
        {
            var dias = CalcularDiasAtePagamento();
            throw new NotImplementedException();
        }

        protected async Task<int> CalcularDiasAtePagamento()
        {
            var infoBank = await _InfoBankRepository.ObterInfoBank();
            var diaPagamento = infoBank.DiaPagamento;
            var hoje = DateTime.Now;

            if (diaPagamento < hoje)
            {
                diaPagamento = diaPagamento.AddMonths(1);
            }

            int diasRestantes = (diaPagamento - hoje).Days;
            return diasRestantes;
        }


    }
}