using FluentValidation;
using MyFeedback.Webapi.DTOs.Areas;

namespace MyFeedback.Webapi.Validacao.Areas
{
    public class AtualizaAreaDtoValidador : AbstractValidator<AtualizaAreaInputDTO>
    {
        public AtualizaAreaDtoValidador()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("O nome da empresa é obrigatório.")
                .Length(3, 150).WithMessage("O nome da empresa deve ter entre 3 e 150 caractéres.");

            RuleFor(a => a.Descricao)
                .MaximumLength(255).WithMessage("A descrição da empresa não deve ultrapassar 255 caractéres.");

            RuleFor(a => a.EmpresaId)
                .NotEmpty().WithMessage("O Id da empresa deve ser informado e não pode ser 0.");
        }
    }
}