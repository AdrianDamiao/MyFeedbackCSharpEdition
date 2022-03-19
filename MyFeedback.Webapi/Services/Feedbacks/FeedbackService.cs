using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFeedback.Webapi.Models.Feedbacks;

namespace MyFeedback.Webapi.Services.Feedbacks
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;
        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Feedback>> BuscaTodos()
        {
            return await _context.Feedbacks.AsNoTracking().ToListAsync();
        }

        public async Task<Feedback> BuscaPorId(long id)
        {
            var feedbackNoDb = await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);

            if (feedbackNoDb == null)
            {
                throw new Exception("Feedback não encontrado");
            }

            return feedbackNoDb;
        }

        public async Task<Feedback> Cria(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();

            return feedback;
        }

        public async Task<Feedback> Deleta(long id)
        {
            var feedbackNoDb = await BuscaPorId(id);

            if (feedbackNoDb == null)
            {
                throw new Exception("Feedback não encontrado");
            }

            _context.Feedbacks.Remove(feedbackNoDb);
            await _context.SaveChangesAsync();

            return feedbackNoDb;
        }
    }
}