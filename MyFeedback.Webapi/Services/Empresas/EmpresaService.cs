using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.Models.Empresas;

namespace MyFeedback.Webapi.Services.Empresas
{
    public class EmpresaService : IEmpresaService
    {
        private readonly ApplicationDbContext _context;
        public EmpresaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Empresa>> BuscaTodos()
        {
            return await _context.Empresas.AsNoTracking().ToListAsync();
        }

        public async Task<Empresa> BuscaPorId(long id)
        {
            var empresaNoDb = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);

            if (empresaNoDb == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            return empresaNoDb;
        }

        public async Task<Empresa> Cria(Empresa empresa)
        {
            await _context.Empresas.AddAsync(empresa);
            await _context.SaveChangesAsync();

            return empresa;
        }

        public async Task<Empresa> Atualiza(long id, Empresa empresa)
        {
            var empresaNoDb = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);

            if (empresaNoDb == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            empresaNoDb = empresa;

            _context.Empresas.Update(empresaNoDb);
            await _context.SaveChangesAsync();

            return empresaNoDb;
        }

        public async Task<Empresa> Deleta(long id)
        {
            var empresaNoDb = await _context.Empresas.FirstOrDefaultAsync(e => e.Id == id);

            if (empresaNoDb == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            _context.Empresas.Remove(empresaNoDb);
            await _context.SaveChangesAsync();

            return empresaNoDb;
        }
    }
}