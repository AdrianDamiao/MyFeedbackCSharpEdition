using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Get()
        {
            var resultados = await _feedbackService.BuscaTodos();

            if(resultados == null)
            {
                return NotFound("Nenhum feedback encontrado");
            }

            var feedbacks = _mapper.Map<List<BuscaTodosFeedbacksOutputDTO>>(resultados);

            return Ok(new { Mensagem = "Feedbacks encontrados", Feedbacks = feedbacks });
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