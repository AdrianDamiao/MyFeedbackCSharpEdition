using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyFeedback.Webapi.ExtensionMethods
{
    public static class Paginador
    {
        public static async Task<PagedModel<TModel>> PaginaAsync<TModel>(
            this IQueryable<TModel> query,
            int pagina,
            int limite = 5)
            where TModel : class
        {
            
            var resultado = new PagedModel<TModel>();

            pagina = (pagina < 0 ) ? 1 : pagina;

            resultado.PaginaAtual = pagina;
            resultado.TamanhoPagina = limite;
            resultado.TotalItens = query.Count();

            var startRow = (pagina - 1) * limite;
            resultado.Itens = await query.Skip(startRow).Take(limite).ToListAsync();
            resultado.TotalPaginas = (int)Math.Ceiling(resultado.TotalItens / (double)limite); 

            return resultado;
        }
    }
}