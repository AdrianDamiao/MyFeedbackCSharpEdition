using AutoMapper;
using MyFeedback.Webapi.DTOs.Areas;
using MyFeedback.Webapi.Models.Areas;

namespace MyFeedback.Webapi.Mapeamentos 
{
    public class AreaProfile : Profile
    {
        public AreaProfile()
	    {
		    CreateMap<Area, Area>();

            CreateMap<CriaAreaInputDTO, Area>()
                .ForMember(a => a.Empresa, opt => opt.Ignore());
		    
            CreateMap<AtualizaAreaInputDTO, Area>()
                .ForMember(a => a.Empresa, opt => opt.Ignore());

            CreateMap<Area, BuscaAreaOutputDTO>()
                .ForMember(a => a.Empresa, opt => opt.Ignore());

            CreateMap<Area, BuscaTodasAreasOutputDTO>();
	    }
    }
}