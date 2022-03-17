namespace MyFeedback.Webapi.DTOs.Colaboradores
{
    public class BuscaTodosColaboradoresOutputDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public long FuncaoId { get; set; }
        public long AreaId { get; set; }
    }
}