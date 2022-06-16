using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public interface IRepositorioFuncionario
    {
        ValidationResult  Inserir(Funcionario funcionario);
        ValidationResult  Editar(Funcionario funcionario);
        ValidationResult  Excluir(Funcionario funcionario);
        List<Funcionario>  SelecionarTodos();
        Funcionario SelecionarPorId(Funcionario funcionario);
    }
}
