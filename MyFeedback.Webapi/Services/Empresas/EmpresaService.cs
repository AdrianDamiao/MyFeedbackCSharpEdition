using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.ExtensionMethods;
using MyFeedback.Webapi.Models.Empresas;

namespace MyFeedback.Webapi.Services.Empresas
{
    public class EmpresaService : IEmpresaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public EmpresaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedModel<Empresa>> BuscaTodosPaginados(int pagina, int limite)
        {
            return await _context.Empresas.AsNoTracking()
                                          .OrderBy(e => e.Id)
                                          .PaginaAsync(pagina, limite);
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

        public async Task<Empresa> Atualiza(Empresa empresa)
        {
            var empresaNoDb = await BuscaPorId(empresa.Id);

            if (empresaNoDb == null)
            {
                throw new Exception("Empresa não encontrada");
            }

            _mapper.Map(empresa, empresaNoDb);

            _context.Empresas.Update(empresaNoDb);
            await _context.SaveChangesAsync();

            return empresaNoDb;
        }

        public async Task<Empresa> Deleta(long id)
        {
            var empresaNoDb = await BuscaPorId(id);

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