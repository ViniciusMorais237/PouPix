using API.Entities;
using API.Interfaces.Repositories;
using Dapper;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Repositories
{
    public class PaginaInicialRepository : BaseRepository, IPaginaInicialRepository
    {
        public PaginaInicialRepository(IDbConnection connection) : base(connection) { }


        public async Task<bool> EditarIndicadoresPaginaInicial(string query, DynamicParameters param)
        {
            // using var connection = new SqlConnection(_connectionString);
            // await connection.OpenAsync();
            // return await connection.ExecuteAsync(query, param) > 0;
            throw new NotImplementedException();
        }


        public async Task<IndicadoresPaginaInicial> ObterIndicadoresPaginaInicial()
        {
            //     using var connection = new SqlConnection(_connectionString);
            //     await connection.OpenAsync();

            //     var sql = @"SELECT 
            //      [NSU_INDICADORES] AS Id
            //     ,[NU_SALDO] AS Saldo
            //     ,[NU_RENDA] AS Renda
            //     ,[NU_LIMITE_CREDITO] AS LimiteCredito
            //     ,[NU_LIMITE_CREDITO_TOTAL] AS LimiteCreditoTotal
            //     ,[DATA_PAGAMENTO] AS DiaPagamento
            // FROM [GerenciadorDeDenhero].[dbo].[Indicadores]";

            //     return (await connection.QueryAsync<IndicadoresPaginaInicial>(sql, param: null, commandType: System.Data.CommandType.Text)).First();
            throw new NotImplementedException();
        }

    }
}