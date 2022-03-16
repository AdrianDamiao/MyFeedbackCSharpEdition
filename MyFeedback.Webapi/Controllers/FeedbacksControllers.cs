using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Feedbacks;
using MyFeedback.Webapi.Services.Feedbacks;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var feedbacks = await _feedbackService.BuscaTodos();

            return Ok(new { Mensagem = "Feedbacks encontrados.", Feedbacks = feedbacks });
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var feedback = _feedbackService.BuscaPorId(id);

            return Ok(new { Mensagem = "Feedback encontrado.", Feedback = feedback });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Feedback feedback)
        {
            if(feedback == null)
            {
                return BadRequest("Feedback não informado");
            }

            var feedbackNovo = await _feedbackService.Cria(feedback);

            return Ok(new { Mensagem = "Feedback cadastrado com sucesso.", Feedback = feedbackNovo });
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            var feedbackNoDb = _feedbackService.Deleta(id);

            return Ok(new { Mensagem = "Feedback removido com sucesso.", Feedback = feedbackNoDb });
        }
    }    
}