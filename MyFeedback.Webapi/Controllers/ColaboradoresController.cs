using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.DTOs.Colaboradores;
using MyFeedback.Webapi.Models.Colaboradores;
using MyFeedback.Webapi.Services.Colaboradores;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColaboradoresController : ControllerBase
    {
        private readonly IColaboradorService _colaboradorService;
        private readonly IMapper _mapper;

        public ColaboradoresController(IColaboradorService colaboradorService, IMapper mapper)
        {
            _colaboradorService = colaboradorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] BuscaTodosColaboradoresInputDTO inputDTO)
        {
            var resultadoPaginado = await _colaboradorService.BuscaTodosPaginado(inputDTO.Pagina,
                                                                                 inputDTO.Limite);

            if(resultadoPaginado == null)
            {
                return NotFound("Nenhum colaborador encontrado");
            }

            var colaboradores = _mapper.Map<List<BuscaTodosColaboradoresOutputDTO>>(resultadoPaginado.Itens);

            var resposta = new PagedModel<BuscaTodosColaboradoresOutputDTO>
            {
                PaginaAtual = resultadoPaginado.PaginaAtual,
                TotalPaginas = resultadoPaginado.TotalPaginas,
                TamanhoPagina = resultadoPaginado.TamanhoPagina,
                TotalItens = resultadoPaginado.TotalItens,
                Itens = colaboradores
            };

            return Ok(new { Mensagem = "Colaboradores encontrados", Colaboradores = resposta });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var resultado = await _colaboradorService.BuscaPorId(id);

            var colaborador = _mapper.Map<BuscaColaboradorOutputDTO>(resultado);

            return Ok(new { Mensagem = "Colaborador encontrado", Colaborador = colaborador });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriaColaboradorInputDTO inputDTO)
        {
            if(inputDTO == null)
            {
                return BadRequest("Colaborador n??o informado");
            }

            var novoColaborador = await _colaboradorService.Cria(_mapper.Map<Colaborador>(inputDTO));

            return Ok(new { Mensagem = "Colaborador cadastrado com sucesso.", Colaborador = novoColaborador });
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] AtualizaColaboradorInputDTO inputDTO)
        {
            var colaborador = _mapper.Map<Colaborador>(inputDTO);
            colaborador.Id = id;

            await _colaboradorService.Atualiza(colaborador);

            return Ok(new { Mensagem = "Colaborador atualizado com sucesso.", Colaborador = inputDTO });
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