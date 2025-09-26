using System.Reflection;
using Dapper;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class PaginaInicialService : IPaginaInicialService
    {
        private readonly IPaginaInicialRepository _paginaInicialRepository;
        protected string _tabela = "[GerenciadorDeDenhero].[dbo].[Indicadores]";
        private readonly Dictionary<string, string> mapeamentoBanco = new Dictionary<string, string>
        {
            {"Saldo", "NU_SALDO"},
            {"Renda", "NU_RENDA"},
            {"LimiteCredito", "NU_LIMITE_CREDITO"},
            {"LimiteCreditoTotal", "NU_LIMITE_CREDITO_TOTAL"},
            {"DiaPagamento", "DATA_PAGAMENTO"}
        };
        public PaginaInicialService(IPaginaInicialRepository paginaInicialRepository)
        {
            _paginaInicialRepository = paginaInicialRepository;
            
        }

        public async Task<bool> EditarIndicadoresPaginaInicial(EdicaoIndicadoresDto edicaoIndicadores)
        {
            var camposEditados = RetornarCamposEditados(edicaoIndicadores);
            var (query, param) = MontarUpdateQuery(camposEditados, mapeamentoBanco);
            return await _paginaInicialRepository.EditarIndicadoresPaginaInicial(query, param);

        }

        public async Task<IndicadoresPaginaInicial> ObterIndicadoresPaginaInicial()
        {
            return await _paginaInicialRepository.ObterIndicadoresPaginaInicial();
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

    }
}