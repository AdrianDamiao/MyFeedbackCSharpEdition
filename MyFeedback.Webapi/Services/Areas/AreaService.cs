using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.Models.Areas;

namespace MyFeedback.Webapi.Services.Areas
{
    public class AreaService : IAreaService
    {
        ApplicationDbContext _context;
        public AreaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> BuscaTodos()
        {
            return await _context.Areas.AsNoTracking().ToListAsync();
        }

        public async Task<Area> BuscaPorId(long id)
        {
            var areaNoDb = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);

            if(areaNoDb == null)
            {
                throw new Exception("Área não encontrada");
            }

            return areaNoDb;
        }

        public async Task<Area> Cria(Area area)
        {
            await _context.Areas.AddAsync(area);
            await _context.SaveChangesAsync();
            
            return area;
        }

        public async Task<Area> Atualiza(long id, Area area)
        {
            var areaNoDb = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);

            if(areaNoDb == null)
            {
                throw new Exception("Área não encontrada");
            }

            areaNoDb = area;

            _context.Areas.Update(areaNoDb);
            await _context.SaveChangesAsync();

            return areaNoDb;
        }

        public async Task<Area> Deleta(long id)
        {
            var areaNoDb = await _context.Areas.FirstOrDefaultAsync(a => a.Id == id);

            if(areaNoDb == null)
            {
                throw new Exception("Área não encontrada");
            }

            _context.Areas.Remove(areaNoDb);
            await _context.SaveChangesAsync();

            return areaNoDb;
        }
    }
}