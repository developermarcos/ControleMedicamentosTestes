using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public interface IRepositorioFuncionario
    {
        string sqlInserir { get; }
        string sqlEditar { get; }
        string sqlExcluir { get; }
        string sqlSelecionarTodos { get; }
        string sqlSelecionarPorId { get; }
        ValidationResult  Inserir(Funcionario funcionario);
        ValidationResult  Editar(Funcionario funcionario);
        ValidationResult  Excluir(Funcionario funcionario);
        List<Funcionario>  SelecionarTodos();
        Funcionario SelecionarPorId(Funcionario funcionario);
        Funcionario ConverterParaFuncionario(SqlDataReader leitorFuncionario);
        void ConfigurarParametrosFuncionario(Funcionario novoFuncionario, SqlCommand comando);
    }
}
