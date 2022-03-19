using System.Collections.Generic;
using MyFeedback.Webapi.Models.Colaboradores;
using MyFeedback.Webapi.Models.Empresas;

namespace MyFeedback.Webapi.Models.Areas
{
    public class Area
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
        public ICollection<Colaborador> Colaboradores { get; set; }
        
        public Area(string nome, string descricao, long empresaId)
        {
            Nome = nome;
            Descricao = descricao;
            EmpresaId = empresaId;
            Colaboradores = new List<Colaborador>();
        }
    }
}