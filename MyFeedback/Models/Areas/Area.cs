namespace MyFeedback.Models.Areas
{
    public class Area
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public long EmpresaId { get; set; }
        
        public Area(string nome, string descricao, long empresaId)
        {
            Nome = nome;
            Descricao = descricao;
            EmpresaId = empresaId;
        }
    }
}