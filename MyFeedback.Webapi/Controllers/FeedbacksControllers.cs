using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Webapi.Models.Feedbacks;

namespace MyFeedback.Webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var feedbacks = _context.Feedbacks.ToList();

            return Ok(feedbacks);
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            var feedback = _context.Feedbacks.FirstOrDefault(f => f.Id == id);

            if(feedback == null)
            {
                return NotFound("Feedback não encontrado.");
            }
            return Ok(feedback);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();

            return Ok(new { Mensagem = "Feedback cadastrado com sucesso."});
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            var feedbackNoDb = _context.Feedbacks.FirstOrDefault(f => f.Id == id);

            if(feedbackNoDb == null)
            {
                return NotFound("Feedback inexistente.");
            }

            _context.Feedbacks.Remove(feedbackNoDb);

            return Ok(new { Mensagem = "Feedback removido com sucesso." });
        }
    }    
}