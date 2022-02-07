using System.Collections.Generic;
using MyFeedback.Models.Funcoes;
using MyFeedback.Models.Feedbacks;

namespace MyFeedback.Models.Colaboradores
{
    public class Colaborador
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public long FuncaoId { get; set; }
        public Funcao Funcao { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        
        public Colaborador(string nome, string email, string senha, long funcaoId)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            FuncaoId = funcaoId;
            Feedbacks = new List<Feedback>();
        }
    }
}