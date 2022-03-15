using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Areas;
using MyFeedback.Webapi.Services.Areas;

namespace MyFeedback.Webapi.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreasController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var areas = await _areaService.BuscaTodos();

            if(areas == null)
            {
                return NotFound("Nenhuma área encontrada");
            }

            return Ok(areas);
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var area = await _areaService.BuscaPorId(id);

            return Ok(area);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Area area)
        {
            if(area == null)
            {
                return BadRequest("Área não informada");
            }

            var novaArea = await _areaService.Cria(area);

            return Ok(new { Mensagem = "Área cadastrada com sucesso.", Area = novaArea });    
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put([FromBody] Area area, long id)
        {
            await _areaService.Atualiza(area);

            return Ok(new { Mensagem = "Área atualizada com sucesso."});
        }

        [HttpDelete]
        [Route("{id:long}")]
        public Task<IActionResult> Delete(long id)
        {
            var areaNoDb = await _areaService.Deleta(id);
           
            return Ok(new { Mensagem = "Área removida com sucesso." });
        }
    }    
}