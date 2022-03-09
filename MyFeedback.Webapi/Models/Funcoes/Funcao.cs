using System.Collections.Generic;
using MyFeedback.Webapi.Models.Colaboradores;

namespace MyFeedback.Webapi.Models.Funcoes
{
    public class Funcao
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public ICollection<Colaborador> Colaboradores { get; set; }
        
        public Funcao(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
            Colaboradores = new List<Colaborador>();
        }
    }
}