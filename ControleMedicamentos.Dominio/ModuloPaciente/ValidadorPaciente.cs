using System;
using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class ValidadorPaciente: AbstractValidator<Paciente>
    {
        public ValidadorPaciente()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("Campo 'Nome' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Nome' não pode ser vazio.")
                .MinimumLength(6).WithMessage("Campo 'Nome' deve conter pelo menos 6 digitos.");

            RuleFor(x => x.CartaoSUS)
                .NotNull().WithMessage("Campo 'CartaoSUS' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'CartaoSUS' não pode ser vazio.")
                .Must(x => x == null || x.Length != 2);
        }
    }
}
