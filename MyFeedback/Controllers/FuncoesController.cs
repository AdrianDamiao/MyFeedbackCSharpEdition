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
            try
            {
                var funcoes = _context.Funcoes.ToList();

                return Ok(funcoes);
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
                var funcao = _context.Funcoes.FirstOrDefault(f => f.Id == id);

                if(funcao == null)
                {
                    return Conflict("Função não encontrada.");
                }

                return Ok(funcao);
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Funcao funcao)
        {
            try
            {
                _context.Funcoes.Add(funcao);
                _context.SaveChanges();

                return Ok(new { Mensagem = "Função cadastrada com sucesso."});
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromBody] Funcao funcao, long id)
        {
            try
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
                var funcaoNoDb = _context.Funcoes.FirstOrDefault(f => f.Id == id);

                if(funcaoNoDb == null)
                {
                    return NotFound("Função inexistente.");
                }

                _context.Funcoes.Remove(funcaoNoDb);

                return Ok(new { Mensagem = "Função removida com sucesso." });
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }    
}