using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Models.Areas;

namespace MyFeedback.Controllers
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
            try
            {
                var areas = _context.Areas.ToList();

                return Ok(areas);
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(long id)
        {
            try
            {
                var area = _context.Areas.FirstOrDefault(a => a.Id == id);

                if(area == null)
                {
                    return NotFound("Área não encontrada.");
                }
                
                return Ok(area);
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Area area)
        {
            try
            {
                _context.Areas.Add(area);
                _context.SaveChanges();

                return Ok(new { Mensagem = "Área cadastrada com sucesso."});
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromBody] Area area, long id)
        {
            try
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
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var areaNoDb = _context.Areas.FirstOrDefault(a => a.Id == id);

                if(areaNoDb == null)
                {
                    return NotFound("Área inexistente.");
                }

                _context.Areas.Remove(areaNoDb);

                return Ok(new { Mensagem = "Área removida com sucesso." });
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }    
}