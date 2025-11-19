using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Dto;
using API.Interfaces.Repositories;
using API.Interfaces.Services;
using AutoMapper;

namespace API.Services
{
    public class AnotacoesService : IAnotacoesService
    {
        private readonly IAnotacoesRepository _repository;
        private readonly IMapper _mapper;

        public AnotacoesService(IAnotacoesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Anotacao> Anotar(AnotacaoCreateDTO anotacao)
        {
            Anotacao anotacaoMapeada = _mapper.Map<Anotacao>(anotacao);

            if (anotacao.Imagem != null)
            {
                anotacaoMapeada.ImagemUrl = ImagemService.CriarArquivoDeImagem(anotacao.Imagem);
            }

            var anotacaoInserida = await _repository.InserirAnotacaoRetornando(anotacaoMapeada) ?? throw new ArgumentNullException("Anotação deu pobrema");

            return anotacaoInserida;
        }

        public async Task<IEnumerable<AnotacaoResponseDTO?>> ObterAnotacoesPorDia(DateTime? date)
        {
            var anotacoes = await _repository.ObterAnotacoesPorDia(date);
            return _mapper.Map<IEnumerable<AnotacaoResponseDTO>>(anotacoes);
        }
    }
}