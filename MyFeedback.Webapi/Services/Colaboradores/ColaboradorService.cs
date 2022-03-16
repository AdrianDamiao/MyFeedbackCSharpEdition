using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.Models.Colaboradores;

namespace MyFeedback.Webapi.Services.Colaboradores
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly ApplicationDbContext _context;
        public ColaboradorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Colaborador>> BuscaTodos()
        {
            return await _context.Colaboradores.AsNoTracking().ToListAsync();
        }

        public async Task<Colaborador> BuscaPorId(long id)
        {
            var colaboradorNoDb = await _context.Colaboradores.FirstOrDefaultAsync(c => c.Id == id);

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

        public async Task<Colaborador> Atualiza(long id, Colaborador colaborador)
        {
            var colaboradorNoDb = await _context.Colaboradores.FirstOrDefaultAsync(c => c.Id == id);

            if (colaboradorNoDb == null)
            {
                throw new Exception("Colaborador não encontrado");
            }

            colaboradorNoDb = colaborador;

            _context.Colaboradores.Update(colaboradorNoDb);
            await _context.SaveChangesAsync();

            return colaboradorNoDb;
        }

        public async Task<Colaborador> Deleta(long id)
        {
            var colaboradorNoDb = await _context.Colaboradores.FirstOrDefaultAsync(c => c.Id == id);

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