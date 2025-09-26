using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class ComprasService : IComprasService
    {
        private readonly IComprasRepository _comprasRepository;

        public ComprasService(IComprasRepository comprasRepository)
        {
            _comprasRepository = comprasRepository;
        }

        public async Task<IEnumerable<CategoriaCompra>> GetCategorias()
        {
            return await _comprasRepository.GetCategorias();
        }

        public async Task<IEnumerable<HistoricoCompra>> GetHistoricoCompras(string date)
        {
            return await _comprasRepository.GetHistoricoCompras(date);
        }

        public async Task<bool> PostCategoria(string nome)
        {
            return await _comprasRepository.PostCategoria(nome);
        }

        public async Task<bool> PostCompra(CompraInsertDTO compra)
        {
            return await _comprasRepository.PostCompra(compra);
        }
    }
}