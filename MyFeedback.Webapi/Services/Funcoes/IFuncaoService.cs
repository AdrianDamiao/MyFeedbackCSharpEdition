using System.Collections.Generic;
using System.Threading.Tasks;
using MyFeedback.Webapi.Models.Funcoes;

namespace MyFeedback.Webapi.Services.Funcoes
{
    public interface IFuncaoService
    {
        Task<PagedModel<Funcao>> BuscaTodosPaginados(int pagina, int limite);
        Task<Funcao> BuscaPorId(long id);
        Task<Funcao> Cria(Funcao funcao);
        Task<Funcao> Atualiza(Funcao funcao);
        Task<Funcao> Deleta(long id);
    }
}