using MyFeedback.Models.Colaboradores;

namespace MyFeedback.Models.Feedbacks
{
    public class Feedback
    {
        public long Id { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }
        public long ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }

        public Feedback(int nota, string comentario, long colaboradorId)
        {
            Nota = nota;
            Comentario = comentario;
            ColaboradorId = colaboradorId;
        }
    }
}