using ControleFornecedors.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public interface IRepositorioFornecedor
    {
        ValidationResult Inserir(Fornecedor fornecedor);
        ValidationResult Editar(Fornecedor fornecedor);
        ValidationResult Excluir(Fornecedor fornecedor);
        List<Fornecedor> SelecionarTodos();
        Fornecedor SelecionarPorId(Fornecedor fornecedor);
    }
}
