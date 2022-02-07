using MyFeedback.Models.Colaboradores;

namespace MyFeedback.Models.Sessoes
{
    public class Sessao
    {
        public long Id { get; set; }
        public string Token { get; set; }
        public string Status { get; set; }
        public long ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }

        public Sessao(string token, string status, long colaboradorId)
        {
            Token = token;
            Status = status;
            ColaboradorId = colaboradorId;
        }
    }
}