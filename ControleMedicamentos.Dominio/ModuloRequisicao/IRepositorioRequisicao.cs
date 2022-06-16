using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ControleRequisicaos.Dominio.ModuloRequisicao
{
    public interface IRepositorioRequisicao
    {
        string sqlInserir { get; }
        string sqlEditar { get; }
        string sqlExcluir { get; }
        string sqlSelecionarTodos { get; }
        string sqlSelecionarPorId { get; }
        ValidationResult Inserir(Requisicao requisicao);
        ValidationResult Editar(Requisicao requisicao);
        ValidationResult Excluir(Requisicao requisicao);
        List<Requisicao> SelecionarTodos();
        Requisicao SelecionarPorId(Requisicao requisicao);
        Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao);
        void ConfigurarParametrosRequisicao(Requisicao novoRequisicao, SqlCommand comando);
    }
}
