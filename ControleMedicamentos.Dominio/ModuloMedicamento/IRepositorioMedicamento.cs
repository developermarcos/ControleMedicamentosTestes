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
        ValidationResult Inserir(Medicamento medicamento);
        ValidationResult Editar(Medicamento medicamento);
        ValidationResult Excluir(Medicamento medicamento);
        List<Medicamento> SelecionarTodos();
        Medicamento SelecionarPorId(Medicamento medicamento);

        Medicamento SelecionarRequisicoesMedicamento(Medicamento medicamento);
    }
}
