namespace MyFeedback.Webapi.DTOs.Areas
{
    public class BuscaTodasAreasOutputDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long EmpresaId { get; set; }
    }
}