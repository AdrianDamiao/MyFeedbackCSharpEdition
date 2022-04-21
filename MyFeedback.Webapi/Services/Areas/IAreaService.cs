using System.Threading.Tasks;
using MyFeedback.Webapi.Models.Areas;

namespace MyFeedback.Webapi.Services.Areas
{
    public interface IAreaService
    {
        Task<PagedModel<Area>> BuscaTodosPaginado(int pagina, int limite);
        Task<Area> BuscaPorId(long id);
        Task<Area> Cria(Area area);
        Task<Area> Atualiza(Area area);
        Task<Area> Deleta(long id);
    }
}