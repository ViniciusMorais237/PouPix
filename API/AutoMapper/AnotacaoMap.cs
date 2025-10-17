using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Entities.Dto;
using API.Services;
using AutoMapper;

namespace API.AutoMapper
{
    public class AnotacaoMap : Profile
    {
        public AnotacaoMap()
        {
            CreateMap<AnotacaoCreateDTO, Anotacao>();
            CreateMap<Anotacao, AnotacaoResponseDTO>()
            .ForMember(dest => dest.ImagemBase64,
            opt => opt.MapFrom(src => ImagemService.LerImagemComoBase64(src.ImagemUrl)));
        }
    }
}