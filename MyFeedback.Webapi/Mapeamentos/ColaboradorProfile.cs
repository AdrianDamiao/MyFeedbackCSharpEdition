using AutoMapper;
using MyFeedback.Webapi.DTOs.Colaboradores;
using MyFeedback.Webapi.Models.Colaboradores;

namespace MyFeedback.Webapi.Mapeamentos
{
    public class ColaboradorProfile : Profile
    {
        public ColaboradorProfile()
        {
            CreateMap<Colaborador, Colaborador>();

            CreateMap<CriaColaboradorInputDTO, Colaborador>();

            CreateMap<AtualizaColaboradorInputDTO, Colaborador>()
                .ForMember(c => c.Funcao, opt => opt.Ignore())
                .ForMember(c => c.Area, opt => opt.Ignore());

            CreateMap<Colaborador, BuscaColaboradorOutputDTO>();

            CreateMap<Colaborador, BuscaTodosColaboradoresOutputDTO>();
        }
    }
}