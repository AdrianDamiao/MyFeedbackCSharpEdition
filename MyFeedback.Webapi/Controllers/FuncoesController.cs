using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Funcoes;
using MyFeedback.Webapi.Services.Funcoes;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncoesController : ControllerBase
    {
        private readonly IFuncaoService _funcaoService;

        public FuncoesController(IFuncaoService funcaoService)
        {
            _funcaoService = funcaoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var funcoes = await _funcaoService.BuscaTodos();

            return Ok(new { Mensagem = "Funções encontradas.", Funcoes = funcoes });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var funcao = await _funcaoService.BuscaPorId(id);

            return Ok(new { Mensagem = "Função encontrada.", Funcao = funcao });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Funcao funcao)
        {
            if(funcao == null)
            {
                return BadRequest("Função não informada");
            }

            var novaFuncao = await _funcaoService.Cria(funcao);

            return Ok(new { Mensagem = "Função cadastrada com sucesso.", Funcao = novaFuncao });
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] Funcao funcao)
        {
            await _funcaoService.Atualiza(id, funcao);

            return Ok(new { Mensagem = "Função atualizada com sucesso.", Funcao = funcao });
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var funcaoNoDb = await _funcaoService.Deleta(id);

            return Ok(new { Mensagem = "Função removida com sucesso.", Funcao = funcaoNoDb });
        }
    }    
}