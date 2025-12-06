using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces.Repositories;
using Dapper;

namespace API.Repositories
{
    public class MoneytarioRepository : BaseRepository, IMoneytarioRepository
    {
        public MoneytarioRepository(IDbConnection connection) : base(connection)
        {
        }

        public async Task<bool> InserirInfoDiariaPadrao(List<InfoMoneyDiario> dias)
        {
           return await GetConnection().ExecuteAsync("INSERT INTO InfoMoneyDiario(DATA, GASTO, LIMITE_DINAMICO, LIMITE_FIXO_BASE) VALUES (@DATA, @GASTO, @LIMITEDINAMICO, @LIMITEFIXO);", dias) > 0;
        }

        public async Task<IEnumerable<Historico?>> ObterHistorico(DateTime data)
        {
            return await GetConnection().QueryAsync<Historico?>(@"SELECT [ID] AS Id
                                                                ,[ID_BANCO] AS IdBanco
                                                                ,[ID_CATEGORIA] AS IdCategoria
                                                                ,[VALOR] AS Valor
                                                                ,[PRESTACAO] AS Prestacao
                                                                ,[DATA] AS Data
                                                            FROM [LifeManagement].[dbo].[HISTORICO]
                                                            WHERE DATA BETWEEN @DATAINICIO AND @DATAFIM
                                                            AND ID_CATEGORIA > 2", new { DATAINICIO = data, DATAFIM = data.AddMonths(1) });
        }

        public async Task<Banco?> ObterInfoBanco(int idBanco)
        {
            return await GetConnection().QueryFirstOrDefaultAsync<Banco>(@"SELECT [ID] AS IdBanco
                                                                    ,[BANCO] AS Nome
                                                                    ,[SALDO] AS Saldo
                                                                FROM [LifeManagement].[dbo].[BANCO]
                                                                WHERE ID = @ID", new { ID = idBanco});
        }

        public async Task<IEnumerable<InfoMoneyDiario>> ObterInfoDiariaMensal(DateTime data)
        {
            return await GetConnection()
            .QueryAsync<InfoMoneyDiario>
            (@"SELECT [DATA] AS Data
            ,[GASTO] AS Gasto
            ,[LIMITE_DINAMICO] AS LimiteDinamico
            ,[LIMITE_FIXO_BASE] AS LimiteFixo
            FROM [LifeManagement].[dbo].[InfoMoneyDiario]
            WHERE DATA BETWEEN @DATAINICIO AND @DATAFIM",
             new { DATAINICIO = data, DATAFIM = data.AddMonths(1) });
        }

        public async Task<InfoMoneyMes> ObterInfoMes(DateTime data)
        {
            return await GetConnection()
            .QueryFirstAsync<InfoMoneyMes>
            (@"SELECT TOP 1
             [ENTRADA] AS Entrada
            ,[INVESTIMENTO] AS PorcentagemInvestimento
            ,[LIMITE_FIXO] AS LimiteFixo
            ,[POUPADO] AS Poupado
            ,[DATA_PAGAMENTO] AS DataPagamento
            FROM [LifeManagement].[dbo].[InfoMoneyMes] WHERE 
            DATA_PAGAMENTO <= @DATA
            ORDER BY DATA_PAGAMENTO DESC",
             new { DATA = data });
        }
    }
}