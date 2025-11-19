using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Dto;

namespace API.Interfaces.Repositories
{
    public interface IAnotacoesRepository
    {
        Task<Anotacao> InserirAnotacaoRetornando(Anotacao anotacao);
        Task<IEnumerable<Anotacao>> ObterAnotacoesPorDia(DateTime? date);
    }
}