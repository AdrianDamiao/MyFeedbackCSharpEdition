using FluentValidation;
using MyFeedback.Webapi.DTOs.Empresas;

namespace MyFeedback.Webapi.Validacao.Empresas
{
    public class CriaEmpresaDtoValidador : AbstractValidator<CriaEmpresaInputDTO>
    {
        public CriaEmpresaDtoValidador(){
            RuleFor(e => e.Nome)
                .NotEmpty().WithMessage("O nome da empresa é obrigatório.")
                .Length(3, 150).WithMessage("O nome da empresa deve ter entre 3 e 150 caractéres.");

            RuleFor(e => e.Site)
                .MaximumLength(255).WithMessage("O site da empresa deve ter até 255 caractéres.");
        }
    }
}