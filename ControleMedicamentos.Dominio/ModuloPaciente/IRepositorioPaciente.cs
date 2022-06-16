using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public interface IRepositorioPaciente
    {
        ValidationResult Inserir(Paciente paciente);
        ValidationResult Editar(Paciente paciente);
        ValidationResult Excluir(Paciente paciente);
        List<Paciente> SelecionarTodos();
        Paciente SelecionarPorId(Paciente paciente);
    }
}
