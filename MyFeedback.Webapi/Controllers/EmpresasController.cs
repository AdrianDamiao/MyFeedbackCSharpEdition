using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Empresas;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmpresasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var empresas = _context.Empresas.ToList();

            return Ok(empresas);
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);

            if(empresa == null)
            {
                return NotFound("Empresa não encontrada.");
            }
            return Ok(empresa);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Empresa empresa)
        {
            _context.Empresas.Add(empresa);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Empresa cadastrada com sucesso."});
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Put([FromBody] Empresa empresa, long id)
        {
            var empresaNoDb = _context.Empresas.FirstOrDefault(e => e.Id == id);

            if(empresaNoDb == null)
            {
                return NotFound("Empresa inexistente.");
            }

            empresaNoDb = empresa;

            _context.Empresas.Update(empresaNoDb);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Empresa atualizada com sucesso."});
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            var empresaNoDb = _context.Empresas.FirstOrDefault(e => e.Id == id);

            if(empresaNoDb == null)
            {
                return NotFound("Empresa inexistente.");
            }

            _context.Empresas.Remove(empresaNoDb);

            return Ok(new { Mensagem = "Empresa removida com sucesso." });
        }
    }    
}