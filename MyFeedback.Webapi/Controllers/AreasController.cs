using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.DTOs.Areas;
using MyFeedback.Webapi.Models.Areas;
using MyFeedback.Webapi.Services.Areas;

namespace MyFeedback.Webapi.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;
        private readonly IMapper _mapper;

        public AreasController(IAreaService areaService, IMapper mapper)
        {
            _areaService = areaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] BuscaTodasAreasInputDTO inputDTO)
        {
            var resultadoPaginado = await _areaService.BuscaTodosPaginado(inputDTO.Pagina,
                                                                          inputDTO.Limite);

            if(resultadoPaginado == null)
            {
                return NotFound("Nenhuma área encontrada");
            }

            var areas = _mapper.Map<List<BuscaTodasAreasOutputDTO>>(resultadoPaginado.Itens);

            var resposta = new PagedModel<BuscaTodasAreasOutputDTO>
            {
                PaginaAtual = resultadoPaginado.PaginaAtual,
                TotalPaginas = resultadoPaginado.TotalPaginas,
                TamanhoPagina = resultadoPaginado.TamanhoPagina,
                TotalItens = resultadoPaginado.TotalItens,
                Itens = areas
            };

            return Ok(new { Mensagem = "Áreas encontradas", Areas = resposta });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var resultado = await _areaService.BuscaPorId(id);

            var area = _mapper.Map<BuscaAreaOutputDTO>(resultado);

            return Ok(new { Mensagem = "Área encontrada", Area = area });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriaAreaInputDTO inputDTO)
        {
            if(inputDTO == null)
            {
                return BadRequest("Área não informada");
            }

            var novaArea = await _areaService.Cria(_mapper.Map<Area>(inputDTO));

            return Ok(new { Mensagem = "Área cadastrada com sucesso", Area = novaArea });    
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] AtualizaAreaInputDTO inputDTO)
        {
            var area = _mapper.Map<Area>(inputDTO);
            area.Id = id;

            await _areaService.Atualiza(area);

            return Ok(new { Mensagem = "Área atualizada com sucesso", Area = inputDTO });
        }

        [HttpDelete]
        [Route("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var areaNoDb = await _areaService.Deleta(id);
           
            return Ok(new { Mensagem = "Área removida com sucesso", Area = areaNoDb });
        }
    }    
}