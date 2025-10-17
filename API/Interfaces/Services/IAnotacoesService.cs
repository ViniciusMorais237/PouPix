using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Dto;

namespace API.Interfaces.Services
{
    public interface IAnotacoesService
    {
        Task<Anotacao> Anotar(AnotacaoCreateDTO anotacao);
        Task<IEnumerable<AnotacaoResponseDTO?>> ObterAnotacoesPorDia(DateTime date);
    }
}