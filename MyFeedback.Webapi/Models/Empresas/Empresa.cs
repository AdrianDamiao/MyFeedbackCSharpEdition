using System.Collections.Generic;
using MyFeedback.Webapi.Models.Areas;

namespace MyFeedback.Webapi.Models.Empresas
{
    public class Empresa
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Site { get; set; }
        public ICollection<Area> Areas { get; set; }

        public Empresa(string nome, string site)
        {
            Nome = nome;
            Site = site;
            Areas = new List<Area>();
        }        
        
    }
}