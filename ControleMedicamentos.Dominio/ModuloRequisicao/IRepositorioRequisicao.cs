using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace ControleRequisicaos.Dominio.ModuloRequisicao
{
    public interface IRepositorioRequisicao
    {
        ValidationResult Inserir(Requisicao requisicao);
        ValidationResult Editar(Requisicao requisicao);
        ValidationResult Excluir(Requisicao requisicao);
        List<Requisicao> SelecionarTodos();
        Requisicao SelecionarPorId(Requisicao requisicao);
    }
}
