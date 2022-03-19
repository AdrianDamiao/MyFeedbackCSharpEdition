using System.Collections.Generic;
using System.Threading.Tasks;
using MyFeedback.Webapi.Models.Colaboradores;

namespace MyFeedback.Webapi.Services.Colaboradores
{
    public interface IColaboradorService
    {
        Task<List<Colaborador>> BuscaTodos();
        Task<Colaborador> BuscaPorId(long id);
        Task<Colaborador> Cria(Colaborador colaborador);
        Task<Colaborador> Atualiza(Colaborador colaborador);
        Task<Colaborador> Deleta(long id);
    }
}