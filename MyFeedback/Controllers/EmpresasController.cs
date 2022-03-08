using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Models.Empresas;

namespace MyFeedback.Controllers
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
            try
            {
                var empresas = _context.Empresas.ToList();

                return Ok(empresas);
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            try
            {
                var empresa = _context.Empresas.FirstOrDefault(e => e.Id == id);

                if(empresa == null)
                {
                    return NotFound("Empresa não encontrada.");
                }
                return Ok(empresa);
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Empresa empresa)
        {
            try
            {
                _context.Empresas.Add(empresa);
                _context.SaveChanges();

                return Ok(new { Mensagem = "Empresa cadastrada com sucesso."});
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Put([FromBody] Empresa empresa, long id)
        {
            try
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
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var empresaNoDb = _context.Empresas.FirstOrDefault(e => e.Id == id);

                if(empresaNoDb == null)
                {
                    return NotFound("Empresa inexistente.");
                }

                _context.Empresas.Remove(empresaNoDb);

                return Ok(new { Mensagem = "Empresa removida com sucesso." });
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }    
}