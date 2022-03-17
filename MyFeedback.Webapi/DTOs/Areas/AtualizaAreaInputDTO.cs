namespace MyFeedback.Webapi.DTOs.Areas
{
    public class AtualizaAreaInputDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long EmpresaId { get; set; }
    }
}