using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Colaboradores;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ColaboradoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var colaboradores = _context.Colaboradores.ToList();

            return Ok(colaboradores);
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var colaborador = _context.Colaboradores.FirstOrDefault(c => c.Id == id);

            if(colaborador == null)
            {
                return NotFound("Colaborador não encontrado.");
            }

            return Ok(colaborador);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Colaborador colaborador)
        {
            _context.Colaboradores.Add(colaborador);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Colaborador cadastrado com sucesso."});
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Put([FromBody] Colaborador colaborador, long id)
        {
            var colaboradorNoDb = _context.Colaboradores.FirstOrDefault(c => c.Id == id);

            if(colaboradorNoDb == null)
            {
                return NotFound("Colaborador inexistente.");
            }

            colaboradorNoDb = colaborador;

            _context.Colaboradores.Update(colaboradorNoDb);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Colaborador atualizado com sucesso."});
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            var colaboradorNoDb = _context.Colaboradores.FirstOrDefault(c => c.Id == id);

            if(colaboradorNoDb == null)
            {
                return NotFound("Colaborador inexistente.");
            }

            _context.Colaboradores.Remove(colaboradorNoDb);

            return Ok(new { Mensagem = "Colaborador removido com sucesso." });
        }
    }    
}