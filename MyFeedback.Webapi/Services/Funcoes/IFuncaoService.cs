using System.Collections.Generic;
using System.Threading.Tasks;
using MyFeedback.Webapi.Models.Funcoes;

namespace MyFeedback.Webapi.Services.Funcoes
{
    public interface IFuncaoService
    {
        Task<List<Funcao>> BuscaTodos();
        Task<Funcao> BuscaPorId(long id);
        Task<Funcao> Cria(Funcao funcao);
        Task<Funcao> Atualiza(long id, Funcao funcao);
        Task<Funcao> Deleta(long id);
    }
}