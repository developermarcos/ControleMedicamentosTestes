using System;
using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    
    public class ValidadorRequisicao : AbstractValidator<Requisicao>
    {
        public ValidadorRequisicao()
        {
            RuleFor(x => x.Data)
                .NotNull().WithMessage("Campo 'Data' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Data' não pode ser vazio.");

            RuleFor(x => x.Data)
                .Must(Data => Data >= DateTime.Now).WithMessage("Data não pode ser menor que data atual.");

            RuleFor(x => x.QtdMedicamento)
                .NotNull().WithMessage("Campo 'QtdMedicamento' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'QtdMedicamento' não pode ser vazio.")
                .GreaterThan(0).WithMessage("Quantidade de medicamento deve ser maior que zero.");
        }
    }
    
}
