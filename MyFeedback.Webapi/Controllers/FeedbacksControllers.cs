using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.DTOs.Feedbacks;
using MyFeedback.Webapi.Models.Feedbacks;
using MyFeedback.Webapi.Services.Feedbacks;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IMapper _mapper;

        public FeedbacksController(IFeedbackService feedbackService, IMapper mapper)
        {
            _feedbackService = feedbackService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery] BuscaTodosFeedbacksInputDTO inputDTO)
        {
            var resultadoPaginado = await _feedbackService.BuscaTodosPaginado(inputDTO.Pagina,
                                                                              inputDTO.Limite);

            if(resultadoPaginado == null)
            {
                return NotFound("Nenhum feedback encontrado");
            }

            var feedbacks = _mapper.Map<List<BuscaTodosFeedbacksOutputDTO>>(resultadoPaginado.Itens);

            var resposta = new PagedModel<BuscaTodosFeedbacksOutputDTO>
            {
                PaginaAtual = resultadoPaginado.PaginaAtual,
                TotalPaginas = resultadoPaginado.TotalPaginas,
                TamanhoPagina = resultadoPaginado.TamanhoPagina,
                TotalItens = resultadoPaginado.TotalItens,
                Itens = feedbacks
            };

            return Ok(new { Mensagem = "Feedbacks encontrados", Feedbacks = resposta });
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var resultado = _feedbackService.BuscaPorId(id);

            var feedback = _mapper.Map<BuscaFeedbackOutputDTO>(resultado);

            return Ok(new { Mensagem = "Feedback encontrado", Feedback = feedback });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CriaFeedbackInputDTO inputDTO)
        {
            if(inputDTO == null)
            {
                return BadRequest("Feedback não informado");
            }

            var feedbackNovo = await _feedbackService.Cria(_mapper.Map<Feedback>(inputDTO));

            return Ok(new { Mensagem = "Feedback cadastrado com sucesso", Feedback = feedbackNovo });
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            var feedbackNoDb = _feedbackService.Deleta(id);

            return Ok(new { Mensagem = "Feedback removido com sucesso", Feedback = feedbackNoDb });
        }
    }    
}