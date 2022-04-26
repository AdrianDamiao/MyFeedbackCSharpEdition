using FluentValidation;
using MyFeedback.Webapi.DTOs.Areas;

namespace MyFeedback.Webapi.Validacao
{
    public class CriaAreaBindingModelValidador : AbstractValidator<CriaAreaInputDTO>
    {
        public CriaAreaBindingModelValidador()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("O nome da empresa deve ser preenchido.")
                .Length(3, 150).WithMessage("O nome da empresa deve ter entre 3 e 150 caractéres");
        }
    }
}