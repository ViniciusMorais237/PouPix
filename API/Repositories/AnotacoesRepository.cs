using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;


namespace API.Repositories
{
    public class AnotacoesRepository : BaseRepository, IAnotacoesRepository
    {
        public AnotacoesRepository(IDbConnection connection) : base(connection)
        {

        }

        public async Task<Anotacao> InserirAnotacaoRetornando(Anotacao anotacao)
        {
            using var connection = GetConnection();
         //   var queryId = "SELECT COUNT(1) FROM [GerenciadorDeDenhero].[dbo].[ANOTACOES]";
          //  anotacao.Id = (await connection.ExecuteScalarAsync<int>(queryId)) + 1;

            var query = @"INSERT INTO [GerenciadorDeDenhero].[dbo].[ANOTACOES]
            (DES_TEXT, DT_HR, URL_IMG) VALUES (@Texto, @Data, @ImagemUrl)";
            var anotacaoInserida = await connection.ExecuteAsync(query, anotacao);

            if (anotacaoInserida > 0)
                return anotacao;

            throw new InvalidOperationException("Anotacao deu pobrema");
        }

        public async Task<IEnumerable<Anotacao>> ObterAnotacoesPorDia(DateTime? date)
        {
            using var connection = GetConnection();
            var where = string.Empty;
            var param = new DynamicParameters();

            if (date.HasValue)
            {
                where = "WHERE CAST(DT_HR AS DATE) = CAST(@DATE AS DATE)";
                param.Add("DATE", date);
            }
            
            var query = $@"SELECT [NU_NSU_ANOT] AS Id
                        ,[DES_TEXT] AS Texto
                        ,[DT_HR] AS Data
                        ,[URL_IMG] AS ImagemUrl
                    FROM [GerenciadorDeDenhero].[dbo].[ANOTACOES] {where}";
            return await connection.QueryAsync<Anotacao>(query, param);
        }
    }
}