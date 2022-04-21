using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.ExtensionMethods;
using MyFeedback.Webapi.Models.Areas;

namespace MyFeedback.Webapi.Services.Areas
{
    public class AreaService : IAreaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public AreaService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedModel<Area>> BuscaTodosPaginado(int pagina, int limite)
        {
            return await _context.Areas.AsNoTracking()
                                       .OrderBy(a => a.Id)
                                       .PaginaAsync(pagina, limite);
        }

        public async Task<Area> BuscaPorId(long id)
        {
            var areaNoDb = await _context.Areas.Where(a => a.Id == id)
                                               .FirstOrDefaultAsync();

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

        public async Task<Area> Atualiza(Area area)
        {
            var areaNoDb = await BuscaPorId(area.Id);

            if(areaNoDb == null)
            {
                throw new Exception("Área não encontrada");
            }

            _mapper.Map(area, areaNoDb);

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