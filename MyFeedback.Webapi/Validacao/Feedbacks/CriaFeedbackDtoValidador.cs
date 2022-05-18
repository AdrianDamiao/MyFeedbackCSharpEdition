using FluentValidation;
using MyFeedback.Webapi.DTOs.Feedbacks;

namespace MyFeedback.Webapi.Validacao.Feedbacks
{
    public class CriaFeedbackDtoValidador : AbstractValidator<CriaFeedbackInputDTO>
    {
        public CriaFeedbackDtoValidador()
        {
            RuleFor(f => f.Nota)
                .NotEmpty().WithMessage("A nota é obrigatória.")
                .ExclusiveBetween(0, 10).WithMessage("A nota atribuida deve estar entre 0 e 10.");

            RuleFor(f => f.Comentario)
                .NotEmpty().WithMessage("O comentário é obrigatório.")
                .MaximumLength(255).WithMessage("O comentário não deve ultrapassar 255 caractéres.");
            
            RuleFor(f => f.ColaboradorId)
                .NotEmpty().WithMessage("O Id do colaborador é obrigatório e não pode ser 0.");
        }
    }
}