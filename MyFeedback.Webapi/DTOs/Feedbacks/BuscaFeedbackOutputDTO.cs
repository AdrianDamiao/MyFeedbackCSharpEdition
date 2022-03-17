using MyFeedback.Webapi.Models.Colaboradores;

namespace MyFeedback.Webapi.DTOs.Feedbacks
{
    public class BuscaFeedbackOutputDTO
    {
        public double Nota { get; set; }
        public string Comentario { get; set; }
        public long ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }
    }
}