using MyFeedback.Webapi.Models.Areas;
using MyFeedback.Webapi.Models.Funcoes;

namespace MyFeedback.Webapi.DTOs.Colaboradores
{
    public class BuscaColaboradorOutputDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public long FuncaoId { get; set; }
        public Funcao Funcao { get; set; }
        public long AreaId { get; set; }
        public Area Area { get; set; }
    }
}