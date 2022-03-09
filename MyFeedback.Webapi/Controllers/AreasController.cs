using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Areas;

namespace MyFeedback.Webapi.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AreasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var areas = _context.Areas.ToList();

            return Ok(areas);
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var area = _context.Areas.FirstOrDefault(a => a.Id == id);

            return Ok(area);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Area area)
        {
            _context.Areas.Add(area);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Área cadastrada com sucesso."});
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Put([FromBody] Area area, long id)
        {
            var areaNoDb = _context.Areas.FirstOrDefault(a => a.Id == id);

            if(areaNoDb == null)
            {
                return NotFound("Área inexistente.");
            }

            areaNoDb = area;

            _context.Areas.Update(areaNoDb);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Área atualizada com sucesso."});
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            var areaNoDb = _context.Areas.FirstOrDefault(a => a.Id == id);

            if(areaNoDb == null)
            {
                return NotFound("Área inexistente.");
            }

            _context.Areas.Remove(areaNoDb);

            return Ok(new { Mensagem = "Área removida com sucesso." });
        }
    }    
}