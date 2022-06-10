using FluentValidation;
using MyFeedback.Webapi.DTOs.Funcoes;

namespace MyFeedback.Webapi.Validacao.Funcoes
{
    public class CriaFuncaoDtoValidador : AbstractValidator<CriaFuncaoInputDTO>
    {
        public CriaFuncaoDtoValidador()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O nome da função é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da função não deve ultrapassar 100 caractéres.");

            RuleFor(f => f.Descricao)
                .MaximumLength(255).WithMessage("A descrição da função não deve ultrapassar 255 caractéres.");
        }
    }
}