using System.Collections.Generic;
using System.Threading.Tasks;
using MyFeedback.Webapi.Models.Feedbacks;

namespace MyFeedback.Webapi.Services.Feedbacks
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> BuscaTodos();
        Task<Feedback> BuscaPorId(long id);
        Task<Feedback> Cria(Feedback feedback);
        Task<Feedback> Deleta(long id);
    }
}