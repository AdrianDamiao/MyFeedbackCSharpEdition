namespace MyFeedback.Webapi.DTOs.Feedbacks
{
    public class CriaFeedbackInputDTO
    {
        public double Nota { get; set; }
        public string Comentario { get; set; }
        public long ColaboradorId { get; set; }
    }
}