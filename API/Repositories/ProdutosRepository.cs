using System.Data;
using API.Entities.Dto;
using API.Entities.Produto;
using API.Interfaces.Repositories;
using API.Repositories.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repositories;

public class ProdutosRepository : BaseRepository, IProdutosRepository
{
    private IDbConnection conexao;
    public ProdutosRepository(IDbConnection connection) : base(connection)
    {
        conexao = connection;
    }

    public async Task<bool> AdicionarDesejo(DesejoDB produto)
    {
        try
        {
            var transacao = BeginTransaction();
            var insert = await conexao.ExecuteAsync(
            "INSERT INTO [LifeManagement].[dbo].[DESEJOS](NO_DESEJO, NU_PRECO_TOTAL, ULR_IMG) VALUES (@Nome, @Valor, @CaminhoImagem)",
            produto,
            transacao)
            > 0;
            Commit();
            return insert;
        }
        catch (SqlException)
        {
            throw;
        }
    }

    public async Task<bool> InserirQuantiaDesejo(int id, int quantia)
    {
        try
        {
            var transaction = BeginTransaction();
            var valorJaDepositado = await conexao.QueryFirstAsync<int?>("SELECT NU_VALOR_DEPOSITADO FROM [LifeManagement].[dbo].[DESEJOS] WHERE NU_NSU_DESEJO = @ID", new { ID = id }, transaction) ?? 0;
            var execucao = await conexao.ExecuteAsync(@"UPDATE [LifeManagement].[dbo].[DESEJOS] SET NU_VALOR_DEPOSITADO = @QUANTIA WHERE NU_NSU_DESEJO = @ID", new { QUANTIA = valorJaDepositado + quantia, ID = id }, transaction) > 0;
            Commit();
            return execucao;
        }
        catch (SqlException)
        {
            throw;
        }

    }


    public async Task<IEnumerable<DesejoDB>> ObterDesejos()
    {
        try
        {
            return await conexao.QueryAsync<DesejoDB>(
            @"SELECT NO_DESEJO AS Nome ,
            NU_PRECO_TOTAL AS Valor,
            NU_VALOR_DEPOSITADO AS ValorDepositado,
            ULR_IMG AS CaminhoImagem 
            FROM [LifeManagement].[dbo].[DESEJOS]");
        }
        catch (SqlException)
        {
            throw;
        }
    }

}
