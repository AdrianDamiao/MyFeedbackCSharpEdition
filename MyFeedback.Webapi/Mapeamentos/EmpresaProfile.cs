using AutoMapper;
using MyFeedback.Webapi.DTOs.Empresas;
using MyFeedback.Webapi.Models.Empresas;

namespace MyFeedback.Webapi.Mapeamentos
{
    public class EmpresaProfile : Profile
    {
        public EmpresaProfile()
        {
            CreateMap<Empresa, Empresa>();

            CreateMap<CriaEmpresaInputDTO, Empresa>();

            CreateMap<AtualizaEmpresaInputDTO, Empresa>();

            CreateMap<Empresa, BuscaEmpresaOutputDTO>();

            CreateMap<Empresa, BuscaTodasEmpresasOutputDTO>();
        }
    }
}