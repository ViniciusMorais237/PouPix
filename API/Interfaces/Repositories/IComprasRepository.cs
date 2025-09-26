using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Dto;

namespace API.Interfaces.Repositories
{
    public interface IComprasRepository
    {
        Task<bool> PostCategoria(string nome);
        Task<IEnumerable<CategoriaCompra>> GetCategorias();
        Task<bool> PostCompra(CompraInsertDTO compra);
        Task<IEnumerable<HistoricoCompra>> GetHistoricoCompras(string date);
    }
}