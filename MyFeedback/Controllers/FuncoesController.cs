using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Models.Funcoes;

namespace MyFeedback.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncoesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FuncoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var funcoes = _context.Funcoes.ToList();

            return Ok(funcoes);
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var funcao = _context.Funcoes.FirstOrDefault(f => f.Id == id);

            if(funcao == null)
            {
                return Conflict("Função não encontrada.");
            }

            return Ok(funcao);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Funcao funcao)
        {
            _context.Funcoes.Add(funcao);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Função cadastrada com sucesso."});
        }

        [HttpPut]
        [Route("{id:long}")]
        public IActionResult Put([FromBody] Funcao funcao, long id)
        {
            var funcaoNoDb = _context.Funcoes.FirstOrDefault(f => f.Id == id);

            if(funcaoNoDb == null)
            {
                return NotFound("Função inexistente.");
            }

            funcaoNoDb = funcao;

            _context.Funcoes.Update(funcaoNoDb);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Função atualizada com sucesso."});
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            var funcaoNoDb = _context.Funcoes.FirstOrDefault(f => f.Id == id);

            if(funcaoNoDb == null)
            {
                return NotFound("Função inexistente.");
            }

            _context.Funcoes.Remove(funcaoNoDb);

            return Ok(new { Mensagem = "Função removida com sucesso." });
        }
    }    
}