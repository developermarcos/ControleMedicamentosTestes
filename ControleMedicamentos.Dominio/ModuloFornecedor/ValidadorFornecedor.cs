using System;
using ControleFornecedors.Dominio.ModuloFornecedor;
using FluentValidation;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public class ValidadorFornecedor : AbstractValidator<Fornecedor>
    {
        public ValidadorFornecedor()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("Campo 'Nome' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo Nome não pode ser vazio.");

            RuleFor(x => x.Telefone)
                .NotNull().WithMessage("Campo 'Telefone' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Telefone' não pode ser vazio.");

            RuleFor(x => x.Estado)
                .NotNull().WithMessage("Campo 'Estado' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Estado' não pode ser vazio.");

            RuleFor(x => x.Cidade)
                .NotNull().WithMessage("Campo 'Cidade' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Cidade' não pode ser vazio.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("Campo 'Email' não pode ser nulo.")
                .NotEmpty().WithMessage("Campo 'Email' não pode ser vazio.")
                .EmailAddress().WithMessage("Formato de Email invalido.");
        }
    }
}
