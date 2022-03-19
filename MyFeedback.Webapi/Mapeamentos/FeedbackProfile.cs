using AutoMapper;
using MyFeedback.Webapi.DTOs.Feedbacks;
using MyFeedback.Webapi.Models.Feedbacks;

namespace MyFeedback.Webapi.Mapeamentos
{
    public class FeedbackProfile : Profile
    {
        public FeedbackProfile()
        {
            CreateMap<Feedback, Feedback>();

            CreateMap<CriaFeedbackInputDTO, Feedback>();

            CreateMap<Feedback, BuscaFeedbackOutputDTO>();

            CreateMap<Feedback, BuscaTodosFeedbacksOutputDTO>();
        }
    }
}