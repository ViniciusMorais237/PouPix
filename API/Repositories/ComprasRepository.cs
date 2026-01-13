using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Repositories;
using Microsoft.OpenApi.Exceptions;

namespace API.Repositories
{
    public class ComprasRepository : BaseRepository, IComprasRepository
    {
        private readonly string TabelaCategoria = "CATEGORIA_COMPRA";
        private readonly string TabelaHistorico = "HISTORICO";
        private readonly Dictionary<string, string> DbCategoriaKeys = new Dictionary<string, string>
        {
            {"Id", "NU_CE_CATEGORIA"},
            {"Nome", "NO_CATEGORIA"}
        };

        private readonly Dictionary<string, string> DbHistoricoKeys = new Dictionary<string, string>
        {
            {"Id", "ID"},
            {"IdBanco", "ID_BANCO"},
            {"IdCategoria", "ID_CATEGORIA"},
            {"Saldo", "SALDO"},
            {"Nome", "NOME"},
            {"Valor", "VALOR"},
            {"Prestacao", "PRESTACAO"},
            {"Data", "DATA"},
        };

        public ComprasRepository(IDbConnection connection) : base(connection) { }

        public async Task<IEnumerable<CategoriaCompra>> GetCategorias()
        {
            return await GetConnection().QueryAsync<CategoriaCompra>("SELECT * FROM CATEGORIA_COMPRA");
        }

        public async Task<IEnumerable<HistoricoCompra>> GetHistoricoCompras(string date)
        {
            var param = new DynamicParameters(new { DATA = date });
            var query = GerarSelectQuery(DbHistoricoKeys, TabelaHistorico);
            query += " WHERE DT_COMPRA = @DATA";
            return await GetAll<HistoricoCompra>(query, param);
        }

        public Task<Dictionary<int, IEnumerable<HistoricoCompra>>> GetHistoricoCompras(DateTime firstDate, DateTime secondDate)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostCategoria(string nome)
        {
            return await GetConnection()
            .ExecuteAsync("INSERT INTO CATEGORIA_COMPRA(NOME) VALUES(@NOME)"
            , new {NOME = nome}) > 0;
        }

        public async Task<bool> PostCompra(CompraInsertDTO compra)
        {
            var dicionarioInsert = GerarDicionarioValoresInseridos(compra, DbHistoricoKeys, new(){"Id"});
            return await Insert(dicionarioInsert, TabelaHistorico);
        }
    }
}