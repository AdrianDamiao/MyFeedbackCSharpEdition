using System.Collections.Generic;
using System.Threading.Tasks;
using MyFeedback.Webapi.Models.Areas;

namespace MyFeedback.Webapi.Services.Areas
{
    public interface IAreaService
    {
        Task<List<Area>> BuscaTodos();
        Task<Area> BuscaPorId(long id);
        Task<Area> Cria(Area area);
        Task<Area> Atualiza(long id, Area area);
        Task<Area> Deleta(long id);
    }
}