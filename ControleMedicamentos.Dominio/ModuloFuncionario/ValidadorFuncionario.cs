using System;
using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class ValidadorFuncionario : AbstractValidator<Funcionario>
    {
        public ValidadorFuncionario()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("Campo 'Nome' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Nome' não pode ser vazio.")
                .MinimumLength(6).WithMessage("Campo 'Nome' deve conter pelo menos 6 digitos."); ;

            RuleFor(x => x.Login)
                .NotNull().WithMessage("Campo 'Login' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Login' não pode ser vazio.")
                .MinimumLength(4).WithMessage("Campo 'Login' deve conter pelo menos 4 digitos.");

            RuleFor(x => x.Senha)
                .NotNull().WithMessage("Campo 'Senha' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Senha' não pode ser vazio.")
                .MinimumLength(6).WithMessage("Campo 'Senha' deve conter pelo menos 6 digitos.");
        }
    }
}
