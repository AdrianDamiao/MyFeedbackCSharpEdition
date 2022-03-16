using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Empresas;
using MyFeedback.Webapi.Services.Empresas;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var empresas = await _empresaService.BuscaTodos();

            return Ok(new { Mensagem = "Empresas encontradas", Empresas = empresas });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var empresa = await _empresaService.BuscaPorId(id);

            return Ok(new { Mensagem = "Empresa encontrada", Empresa = empresa });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Empresa empresa)
        {
            if(empresa == null)
            {
                return BadRequest("Empresa não informada");
            }

            var novaEmpresa = await _empresaService.Cria(empresa);

            return Ok(new { Mensagem = "Empresa cadastrada com sucesso.", Empresa = novaEmpresa});
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] Empresa empresa)
        {
            await _empresaService.Atualiza(id, empresa);

            return Ok(new { Mensagem = "Empresa atualizada com sucesso.", Empresa = empresa});
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var empresaNoDb = await _empresaService.Deleta(id);

            return Ok(new { Mensagem = "Empresa removida com sucesso.", Empresa = empresaNoDb });
        }
    }    
}