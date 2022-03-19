using System.Collections.Generic;
using MyFeedback.Webapi.Models.Funcoes;
using MyFeedback.Webapi.Models.Feedbacks;
using MyFeedback.Webapi.Models.Areas;

namespace MyFeedback.Webapi.Models.Colaboradores
{
    public class Colaborador
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; } //Temporário
        public long FuncaoId { get; set; }
        public Funcao Funcao { get; set; }
        public long AreaId { get; set; }
        public Area Area { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
}