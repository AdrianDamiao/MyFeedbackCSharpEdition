using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.DTOs.Empresas;
using MyFeedback.Webapi.Models.Empresas;
using MyFeedback.Webapi.Services.Empresas;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;

        public EmpresasController(IEmpresaService empresaService, IMapper mapper)
        {
            _empresaService = empresaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] BuscaTodasEmpresasInputDTO inputDTO)
        {
            var resultadoPaginado = await _empresaService.BuscaTodosPaginados(inputDTO.Pagina,
                                                                     inputDTO.Limite);

            if(resultadoPaginado == null)
            {
                return NotFound("Nenhuma empresa encontrada");
            }

            var empresas = _mapper.Map<List<BuscaTodasEmpresasOutputDTO>>(resultadoPaginado.Itens);

            var resposta = new PagedModel<BuscaTodasEmpresasOutputDTO>
            {
                PaginaAtual = resultadoPaginado.PaginaAtual,
                TotalPaginas = resultadoPaginado.TotalPaginas,
                TamanhoPagina = resultadoPaginado.TamanhoPagina,
                TotalItens = resultadoPaginado.TotalItens,
                Itens = empresas
            };

            return Ok(new { Mensagem = "Empresas encontradas", Empresas = resposta });
        }

        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            var resultado = await _empresaService.BuscaPorId(id);

            var empresa = _mapper.Map<BuscaEmpresaOutputDTO>(resultado);

            return Ok(new { Mensagem = "Empresa encontrada", Empresa = empresa });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriaEmpresaInputDTO inputDTO)
        {
            if(inputDTO == null)
            {
                return BadRequest("Empresa não informada");
            }

            var novaEmpresa = await _empresaService.Cria(_mapper.Map<Empresa>(inputDTO));

            return Ok(new { Mensagem = "Empresa cadastrada com sucesso.", Empresa = novaEmpresa});
        }

        [HttpPut]
        [Route("{id:long}")]
        public async Task<IActionResult> Put(long id, [FromBody] AtualizaEmpresaInputDTO inputDTO)
        {
            var empresa = _mapper.Map<Empresa>(inputDTO);
            empresa.Id = id;

            await _empresaService.Atualiza(empresa);

            return Ok(new { Mensagem = "Empresa atualizada com sucesso.", Empresa = inputDTO});
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