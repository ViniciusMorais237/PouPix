using API.Entities;
using API.Interfaces.Repositories;
using Dapper;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repositories
{
    public class InfoBankRepository : BaseRepository, IInfoBankRepository
    {
        private readonly Dictionary<string, string> DbKeys = new ()
        {
            {"Saldo", "NU_SALDO"},
            {"Renda", "NU_RENDA"},
            {"LimiteCredito", "NU_LIMITE_CREDITO"},
            {"LimiteCreditoTotal", "NU_LIMITE_CREDITO_TOTAL"},
            {"Fatura", "NU_FATURA"},
            {"DiaPagamento", "DATA_PAGAMENTO"}
        };
        public InfoBankRepository(IDbConnection connection) : base(connection)
        { }


        public async Task<bool> EditarInfoBank(string query, DynamicParameters param)
        {
            using var connection = GetConnection();
            connection.Open();
            return await connection.ExecuteAsync(query, param) > 0;
        }


        public async Task<InfoBank> ObterInfoBank()
        {
            using var connection = GetConnection();
            connection.Open();

            var sql = GerarSelectQuery(DbKeys, "InfoBank");

            return (await connection.QueryAsync<InfoBank>(sql, param: null, commandType: System.Data.CommandType.Text)).First();
        }

        public async Task<IEnumerable<MonetarioDiario>> ObterInfoMoney()
        {
            throw new NotImplementedException();
        }
    }
}