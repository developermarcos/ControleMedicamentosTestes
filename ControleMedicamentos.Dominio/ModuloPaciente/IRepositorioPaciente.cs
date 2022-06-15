using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public interface IRepositorioPaciente
    {
        string sqlInserir { get; }
        string sqlEditar { get; }
        string sqlExcluir { get; }
        string sqlSelecionarTodos { get; }
        string sqlSelecionarPorId { get; }
        ValidationResult Inserir(Paciente paciente);
        ValidationResult Editar(Paciente paciente);
        ValidationResult Excluir(Paciente paciente);
        List<Paciente> SelecionarTodos();
        Paciente SelecionarPorId(Paciente paciente);
        Paciente ConverterParaPaciente(SqlDataReader leitorPaciente);
        void ConfigurarParametrosPaciente(Paciente novoPaciente, SqlCommand comando);
    }
}
