using FluentValidation;
using MyFeedback.Webapi.DTOs.Colaboradores;

namespace MyFeedback.Webapi.Validacao.Colaboradores
{
    public class CriaColaboradorDtoValidador : AbstractValidator<CriaColaboradorInputDTO>
    {
        public CriaColaboradorDtoValidador()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do colaborador é obrigatório.")
                .Length(3, 100).WithMessage("O nome do colaborador deve ter entre 3 e 100 caractéres.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail do colaborador é obrigatório.")
                .Length(5, 100).WithMessage("O e-mail do colaborador deve ter entre 5 e 100 caractéres.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.");

            RuleFor(c => c.Senha)
                .NotEmpty().WithMessage("A senha do colaborador é obrigatória.");
            
            RuleFor(c => c.FuncaoId)
                .NotEmpty().WithMessage("O Id da Função deve ser informado e não pode ser 0.");

            RuleFor(c => c.AreaId)
                .NotEmpty().WithMessage("O Id da Área deve ser informado e não pode ser 0.");
        }
    }
}