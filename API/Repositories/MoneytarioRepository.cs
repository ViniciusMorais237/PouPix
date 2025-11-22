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

        public async Task<InfoMoneyMes> ObterInfoMes(DateTime data)
        {
            return await GetConnection().QueryFirstAsync<InfoMoneyMes>(@"SELECT TOP 1
                                                         [ENTRADA] AS Entrada
                                                        ,[INVESTIMENTO] AS PorcentagemInvestimento
                                                        ,[LIMITE_FIXO] AS LimiteFixo
                                                        ,[POUPADO] AS Poupado
                                                        ,[DATA_PAGAMENTO] AS DataPagamento
                                                        FROM [LifeManagement].[dbo].[InfoMoneyMes] WHERE 
                                                        DATA_PAGAMENTO <= @DATA", new {DATA = data});
        }
    }
}