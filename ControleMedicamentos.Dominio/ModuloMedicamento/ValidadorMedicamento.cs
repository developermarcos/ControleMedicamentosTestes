using System;
using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class ValidadorMedicamento : AbstractValidator<Medicamento>
    {
        public ValidadorMedicamento()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("Campo 'Nome' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Nome' não pode ser vazio.")
                .MinimumLength(6).WithMessage("Campo 'Nome' deve conter pelo menos 6 digitos.");

            RuleFor(x => x.Descricao)
                .NotNull().WithMessage("Campo 'Descricao' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Descricao' não pode ser vazio.")
                .MinimumLength(10).WithMessage("Campo 'Descricao' deve conter pelo menos 10 digitos.");

            RuleFor(x => x)
                .Must(x => x.QuantidadeDisponivel > 0)
                .WithMessage("Quantidade de medicamento deve ser maior que zero.");

        }
    }
}
