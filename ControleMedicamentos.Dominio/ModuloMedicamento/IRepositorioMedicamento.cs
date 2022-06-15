using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public interface IRepositorioMedicamento
    {
        string sqlInserir { get; }
        string sqlEditar { get; }
        string sqlExcluir { get; }
        string sqlSelecionarTodos { get; }
        string sqlSelecionarPorId { get; }
        ValidationResult Inserir(Medicamento medicamento);
        ValidationResult Editar(Medicamento medicamento);
        ValidationResult Excluir(Medicamento medicamento);
        List<Medicamento> SelecionarTodos();
        Medicamento SelecionarPorId(Medicamento medicamento);
        Medicamento ConverterParaMedicamento(SqlDataReader leitormedicamento);
        void ConfigurarParametrosMedicamento(Medicamento novomedicamento, SqlCommand comando);
    }
}
