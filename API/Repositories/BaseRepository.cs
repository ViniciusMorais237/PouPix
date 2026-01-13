using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using API.Interfaces.Repositories;

namespace API.Repositories
{
    public class BaseRepository
    {
        protected IDbConnection _connection;
        protected IDbTransaction? _transaction;
        public BaseRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDbConnection GetConnection()
        {
            _connection.Open();
            return _connection;
        }

        public IDbTransaction BeginTransaction()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public void Commit()
        {
            _transaction?.Commit();
            _connection.Close();
            _transaction?.Dispose();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<T>> GetAll<T>(string query, DynamicParameters? param = null)
        {
            try
            {
                return await _connection.QueryAsync<T>(query, param);
            }
            catch (SqlException ex)
            {

                throw new Exception($"Falha ao executar query", ex);
            }
        }

        public async Task<T?> GetById<T>(object id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> Insert(Dictionary<string, object> camposInseridos, string tabela)
        {
            var (query, param) = GerarInsertQuery(camposInseridos, tabela);
            try
            {
                var result = await _connection.ExecuteAsync(query, param);
                return result > 0;
            }
            catch (SqlException ex)
            {

                throw new Exception($"Falha ao executar query", ex);
            }
        }

        public Task<bool> Update(Dictionary<string, object> camposEditados, string tabela)
        {
            throw new NotImplementedException();
        }

        protected (string query, DynamicParameters param) GerarInsertQuery(Dictionary<string, object> camposInseridos,
        string tabela)
        {
            var campos = new List<string>();
            var valores = new List<string>();
            var param = new DynamicParameters();

            foreach (var campo in camposInseridos)
            {
                campos.Add(campo.Key);
                valores.Add($"@{campo.Key}");
                param.Add(campo.Key, campo.Value);
            }

            var camposInsert = string.Join(',', campos);
            var valoresInsert = string.Join(',', valores);

            var query = $"INSERT INTO {tabela}({camposInsert}) VALUES ({valoresInsert})";
            return (query, param);
        }

        protected string GerarSelectQuery(Dictionary<string, string> DbKeys,
        string tabela,
        List<string>? camposIgnorados = null)
        {
            var campos = new List<string>();
            foreach (var kvp in DbKeys)
            {
                if (camposIgnorados != null && camposIgnorados.Contains(kvp.Key))
                    continue;

                campos.Add($"{kvp.Value} AS {kvp.Key} ");
            }
            var camposSelect = string.Join(',', campos);
            string query = $"SELECT {camposSelect} FROM {tabela}";
            return query;
        }

        protected Dictionary<string, object> GerarDicionarioValoresInseridos(object obj, Dictionary<string, string> DbKeys,
        List<string>? propriedaadesIgnoradas = null)
        {
            var propriedadesIgnored = propriedaadesIgnoradas ?? new List<string>();

            var resultado = new Dictionary<string, object>();

            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.GetValue(obj) is null || propriedadesIgnored.Contains(property.Name))
                    continue;

                if (DbKeys.TryGetValue(property.Name, out string? nomeColuna))
                {
                    var valor = property.GetValue(obj);
                    if (valor != null)
                        resultado[nomeColuna] = valor;
                }
            }
            return resultado;
        }
    }

}