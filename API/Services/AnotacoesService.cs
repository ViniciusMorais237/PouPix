using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces.Repositories;
using API.Interfaces.Services;

namespace API.Services
{
    public class AnotacoesService : IAnotacoesService
    {
        private readonly IAnotacoesRepository _repository;

        public AnotacoesService(IAnotacoesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Anotacao> Anotar(Anotacao anotacao)
        {
            if (anotacao.Imagem != null)
            {
                anotacao.ImagemTexto = ImagemService.CriarArquivoDeImagem(anotacao.Imagem);
            }
            
            var anotacaoInserida = await _repository.InserirAnotacaoRetornando(anotacao) ?? throw new ArgumentNullException("Anotação deu pobrema");

            return anotacaoInserida;
        }
    }
}