namespace MyFeedback.Webapi.DTOs.Colaboradores
{
    public class AtualizaColaboradorInputDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public long FuncaoId { get; set; }
        public long AreaId { get; set; }
    }
}