using MyFeedback.Webapi.Models.Empresas;

namespace MyFeedback.Webapi.DTOs.Areas
{
    public class BuscaAreaOutputDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}