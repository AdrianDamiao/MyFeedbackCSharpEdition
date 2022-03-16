using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.Models.Funcoes;

namespace MyFeedback.Webapi.Services.Funcoes
{
    public class FuncaoService : IFuncaoService
    {
        private readonly ApplicationDbContext _context;
        public FuncaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Funcao>> BuscaTodos()
        {
            return await _context.Funcoes.AsNoTracking().ToListAsync();
        }

        public async Task<Funcao> BuscaPorId(long id)
        {
            var funcaoNoDb = await _context.Funcoes.FirstOrDefaultAsync(f => f.Id == id);

            if (funcaoNoDb == null)
            {
                throw new Exception("Função não encontrada");
            }

            return funcaoNoDb;
        }

        public async Task<Funcao> Cria(Funcao funcao)
        {
            await _context.Funcoes.AddAsync(funcao);
            await _context.SaveChangesAsync();

            return funcao;
        }

        public async Task<Funcao> Atualiza(long id, Funcao funcao)
        {
            var funcaoNoDb = await _context.Funcoes.FirstOrDefaultAsync(f => f.Id == id);

            if (funcaoNoDb == null)
            {
                throw new Exception("Função não encontrada");
            }

            funcaoNoDb = funcao;

            _context.Funcoes.Update(funcaoNoDb);
            await _context.SaveChangesAsync();

            return funcaoNoDb;
        }

        public async Task<Funcao> Deleta(long id)
        {
            var funcaoNoDb = await _context.Funcoes.FirstOrDefaultAsync(f => f.Id == id);

            if (funcaoNoDb == null)
            {
                throw new Exception("Função não encontrada");
            }

            _context.Funcoes.Remove(funcaoNoDb);
            await _context.SaveChangesAsync();

            return funcaoNoDb;
        }
    }
}