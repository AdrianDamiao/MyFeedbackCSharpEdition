using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.ExtensionMethods;
using MyFeedback.Webapi.Models.Funcoes;

namespace MyFeedback.Webapi.Services.Funcoes
{
    public class FuncaoService : IFuncaoService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public FuncaoService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedModel<Funcao>> BuscaTodosPaginados(int pagina, int limite)
        {
            return await _context.Funcoes.AsNoTracking()
                                         .OrderBy(f => f.Id)
                                         .PaginaAsync(pagina, limite);
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

        public async Task<Funcao> Atualiza(Funcao funcao)
        {
            var funcaoNoDb = await BuscaPorId(funcao.Id);

            if (funcaoNoDb == null)
            {
                throw new Exception("Função não encontrada");
            }

            _mapper.Map(funcao, funcaoNoDb);

            _context.Funcoes.Update(funcaoNoDb);
            await _context.SaveChangesAsync();

            return funcaoNoDb;
        }

        public async Task<Funcao> Deleta(long id)
        {
            var funcaoNoDb = await BuscaPorId(id);

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