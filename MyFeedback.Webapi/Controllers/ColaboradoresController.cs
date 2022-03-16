using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Colaboradores;
using MyFeedback.Webapi.Services.Colaboradores;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;

        public ColaboradoresController(IColaboradorService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var colaboradores = await _colaboradorService.BuscaTodos();

            return Ok(new { Mensagem = "Colaboradores encontrados", Colaboradores = colaboradores });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var colaborador = await _colaboradorService.BuscaPorId(id);

            return Ok(new { Mensagem = "Colaborador encontrado", Colaborador = colaborador });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Colaborador colaborador)
        {
            if(colaborador == null)
            {
                return BadRequest("Colaborador não informado");
            }

            var novoColaborador = await _colaboradorService.Cria(colaborador);

            return Ok(new { Mensagem = "Colaborador cadastrado com sucesso.", Colaborador = novoColaborador });
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] Colaborador colaborador)
        {
            await _colaboradorService.Atualiza(id, colaborador);

            return Ok(new { Mensagem = "Colaborador atualizado com sucesso.", Colaborador = colaborador });
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var colaboradorNoDb = await _colaboradorService.Deleta(id);

            return Ok(new { Mensagem = "Colaborador removido com sucesso.", Colaborador = colaboradorNoDb });
        }
    }    
}