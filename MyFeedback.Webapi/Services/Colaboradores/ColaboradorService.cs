using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.ExtensionMethods;
using MyFeedback.Webapi.Models.Colaboradores;

namespace MyFeedback.Webapi.Services.Colaboradores
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ColaboradorService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedModel<Colaborador>> BuscaTodosPaginado(int pagina, int limite)
        {
            return await _context.Colaboradores.AsNoTracking()
                                               .OrderBy(c => c.Id)
                                               .PaginaAsync(pagina, limite);
        }

        public async Task<Colaborador> BuscaPorId(long id)
        {
            var colaboradorNoDb = await _context.Colaboradores.Where(c => c.Id == id)
                                                              .Include(c => c.Funcao)
                                                              .Include(c => c.Area)  
                                                              .FirstOrDefaultAsync();

            if (colaboradorNoDb == null)
            {
                throw new Exception("Colaborador não encontrado");
            }

            return colaboradorNoDb;
        }

        public async Task<Colaborador> Cria(Colaborador colaborador)
        {
            await _context.Colaboradores.AddAsync(colaborador);
            await _context.SaveChangesAsync();

            return colaborador;
        }

        public async Task<Colaborador> Atualiza(Colaborador colaborador)
        {
            var colaboradorNoDb = await BuscaPorId(colaborador.Id);

            if (colaboradorNoDb == null)
            {
                throw new Exception("Colaborador não encontrado");
            }

            _mapper.Map(colaborador, colaboradorNoDb);

            _context.Colaboradores.Update(colaboradorNoDb);
            await _context.SaveChangesAsync();

            return colaboradorNoDb;
        }

        public async Task<Colaborador> Deleta(long id)
        {
            var colaboradorNoDb = await BuscaPorId(id);

            if(colaboradorNoDb == null)
            {
                throw new Exception("Colaborador não encontrado");
            }

            _context.Colaboradores.Remove(colaboradorNoDb);
            await _context.SaveChangesAsync();

            return colaboradorNoDb;
        }
    }
}