using System.Collections.Generic;
using System.Threading.Tasks;
using MyFeedback.Webapi.Models.Empresas;

namespace MyFeedback.Webapi.Services.Empresas
{
    public interface IEmpresaService
    {
        Task<List<Empresa>> BuscaTodos();
        Task<Empresa> BuscaPorId(long id);
        Task<Empresa> Cria(Empresa empresa);
        Task<Empresa> Atualiza(long id, Empresa empresa);
        Task<Empresa> Deleta(long id);
    }
}