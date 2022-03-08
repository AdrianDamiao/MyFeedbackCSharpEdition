using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFeedback.Models.Feedbacks;

namespace MyFeedback.Controllers
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
            try
            {
                var feedbacks = _context.Feedbacks.ToList();

                return Ok(feedbacks);
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult Get(long id)
        {
            try
            {
                var feedback = _context.Feedbacks.FirstOrDefault(f => f.Id == id);

                if(feedback == null)
                {
                    return NotFound("Feedback não encontrado.");
                }
                return Ok(feedback);
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Feedback feedback)
        {
            try
            {
                _context.Feedbacks.Add(feedback);
                _context.SaveChanges();

                return Ok(new { Mensagem = "Feedback cadastrado com sucesso."});
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:long}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var feedbackNoDb = _context.Feedbacks.FirstOrDefault(f => f.Id == id);

                if(feedbackNoDb == null)
                {
                    return NotFound("Feedback inexistente.");
                }

                _context.Feedbacks.Remove(feedbackNoDb);

                return Ok(new { Mensagem = "Feedback removido com sucesso." });
            }
            catch(Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }    
}