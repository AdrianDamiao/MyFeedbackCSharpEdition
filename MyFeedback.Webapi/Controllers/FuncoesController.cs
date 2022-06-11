using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.DTOs.Empresas;
using MyFeedback.Webapi.DTOs.Funcoes;
using MyFeedback.Webapi.Models.Funcoes;
using MyFeedback.Webapi.Services.Funcoes;

namespace MyFeedback.Webapi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FuncoesController : ControllerBase
    {
        private readonly IFuncaoService _funcaoService;
        private readonly IMapper _mapper;

        public FuncoesController(IFuncaoService funcaoService, IMapper mapper)
        {
            _funcaoService = funcaoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] BuscaTodasFuncoesInputDTO inputDTO)
        {
            var resultadoPaginado = await _funcaoService.BuscaTodosPaginados(inputDTO.Pagina,
                                                                             inputDTO.Limite);

            if(resultadoPaginado == null)
            {
                return NotFound("Nenhuma função encontrada");
            }

            var funcoes = _mapper.Map<List<BuscaTodasFuncoesOutputDTO>>(resultadoPaginado.Itens);

            var resposta = new PagedModel<BuscaTodasFuncoesOutputDTO>
            {
                PaginaAtual = resultadoPaginado.PaginaAtual,
                TotalPaginas = resultadoPaginado.TotalPaginas,
                TamanhoPagina = resultadoPaginado.TamanhoPagina,
                TotalItens = resultadoPaginado.TotalItens,
                Itens = funcoes
            };

            return Ok(new { Mensagem = "Funções encontradas.", Funcoes = resposta });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var resultado = await _funcaoService.BuscaPorId(id);

            var funcao = _mapper.Map<BuscaFuncaoOutputDTO>(resultado);

            return Ok(new { Mensagem = "Função encontrada.", Funcao = funcao });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriaFuncaoInputDTO inputDTO)
        {
            if(inputDTO == null)
            {
                return BadRequest("Função não informada");
            }

            var novaFuncao = await _funcaoService.Cria(_mapper.Map<Funcao>(inputDTO));

            return Ok(new { Mensagem = "Função cadastrada com sucesso.", Funcao = novaFuncao });
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] AtualizaFuncaoInputDTO inputDTO)
        {
            var funcao = _mapper.Map<Funcao>(inputDTO);
            funcao.Id = id;

            await _funcaoService.Atualiza(funcao);

            return Ok(new { Mensagem = "Função atualizada com sucesso.", Funcao = inputDTO });
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