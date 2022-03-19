using AutoMapper;
using MyFeedback.Webapi.DTOs.Funcoes;
using MyFeedback.Webapi.Models.Funcoes;

namespace MyFeedback.Webapi.Mapeamentos
{
    public class FuncaoProfile : Profile
    {
        public FuncaoProfile()
        {
            CreateMap<Funcao, Funcao>();

            CreateMap<CriaFuncaoInputDTO, Funcao>();

            CreateMap<AtualizaFuncaoInputDTO, Funcao>();

            CreateMap<Funcao, BuscaFuncaoOutputDTO>();

            CreateMap<Funcao, BuscaTodasFuncoesOutputDTO>();
        }
    }
}