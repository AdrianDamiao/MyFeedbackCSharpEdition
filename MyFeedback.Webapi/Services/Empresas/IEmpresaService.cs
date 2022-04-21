using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MyFeedback.Webapi.Models.Empresas;

namespace MyFeedback.Webapi.Services.Empresas
{
    public interface IEmpresaService
    {
        Task<PagedModel<Empresa>> BuscaTodosPaginados(int pagina, int limite);
        Task<Empresa> BuscaPorId(long id);
        Task<Empresa> Cria(Empresa empresa);
        Task<Empresa> Atualiza(Empresa empresa);
        Task<Empresa> Deleta(long id);
    }
}