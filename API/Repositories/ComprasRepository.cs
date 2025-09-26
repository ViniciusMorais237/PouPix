using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Repositories;

namespace API.Repositories
{
    public class ComprasRepository : BaseRepository, IComprasRepository
    {
        private readonly string TabelaCategoria = "CATEGORIA_COMPRA";
        private readonly string TabelaHistorico = "HISTORICO_COMPRA";
        private readonly Dictionary<string, string> DbCategoriaKeys = new Dictionary<string, string>
        {
            {"Id", "NU_CE_CATEGORIA"},
            {"Nome", "NO_CATEGORIA"}
        };

        private readonly Dictionary<string, string> DbHistoricoKeys = new Dictionary<string, string>
        {
            {"Id", "NU_NSU_HISTORICO_COMPRA"},
            {"Nome", "DE_NOME"},
            {"Valor", "NU_VALOR"},
            {"IdCategoria", "NU_CE_CATEGORIA"},
            {"Categoria", "NO_CATEGORIA"},
            {"Data", "DT_COMPRA"},
            {"Comentario", "DE_COMENTARIO"}
        };

        public ComprasRepository(IDbConnection connection) : base(connection) { }

        public async Task<IEnumerable<CategoriaCompra>> GetCategorias()
        {
            var query = GerarSelectQuery(DbCategoriaKeys, TabelaCategoria);
            return await GetAll<CategoriaCompra>(query);
        }

        public async Task<IEnumerable<HistoricoCompra>> GetHistoricoCompras(string date)
        {
            var param = new DynamicParameters(new { DATA = date });
            var query = GerarSelectQuery(DbHistoricoKeys, TabelaHistorico);
            query += " WHERE DT_COMPRA = @DATA";
            return await GetAll<HistoricoCompra>(query, param);
        }

        public async Task<bool> PostCategoria(string nome)
        {
            Dictionary<string, object> propriedadeInserida = new Dictionary<string, object>
            {
                {"NO_CATEGORIA", $"{nome}"}
            };

            return await Insert(propriedadeInserida, TabelaCategoria);
        }

        public async Task<bool> PostCompra(CompraInsertDTO compra)
        {
            var dicionarioInsert = GerarDicionarioValoresInseridos(compra, DbHistoricoKeys);
            return await Insert(dicionarioInsert, TabelaHistorico);
        }
    }
}