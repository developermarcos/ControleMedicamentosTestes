using ControleFornecedors.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Dominio.ModuloFornecedor
{
    public interface IRepositorioFornecedor
    {
        string sqlInserir { get; }
        string sqlEditar { get; }
        string sqlExcluir { get; }
        string sqlSelecionarTodos { get; }
        string sqlSelecionarPorId { get; }
        ValidationResult Inserir(Fornecedor fornecedor);
        ValidationResult Editar(Fornecedor fornecedor);
        ValidationResult Excluir(Fornecedor fornecedor);
        List<Fornecedor> SelecionarTodos();
        Fornecedor SelecionarPorId(Fornecedor fornecedor);
        Fornecedor ConverterParaFuncionario(SqlDataReader leitorFornecedor);
        void ConfigurarParametrosFuncionario(Fornecedor novoFornecedor, SqlCommand comando);
    }
}
