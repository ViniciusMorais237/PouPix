using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
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
            var queryId = "SELECT COUNT(1) FROM [GerenciadorDeDenhero].[dbo].[ANOTACOES]";
            anotacao.Id = (await connection.ExecuteScalarAsync<int>(queryId)) + 1;

            var query = @"INSERT INTO [GerenciadorDeDenhero].[dbo].[ANOTACOES]
            (NU_NSU_ANOT, DES_TEXT, DT_HR, URL_IMG) VALUES (@Id, @Texto, @Data, @ImagemTexto)";
            var anotacaoInserida = await connection.ExecuteAsync(query, anotacao);

            if (anotacaoInserida > 0)
                return anotacao;

            throw new InvalidOperationException("Anotacao deu pobrema");
        }
    }
}