using System;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class ValidadorMedicamentoTest
    {
        [TestMethod]
        public void Nome_medicamento_nao_deve_ser_nulo()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            medicamento.Nome = null;

            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("Campo 'Nome' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        #region Métodos privados
        private static Medicamento CriaObjetoMedicamento()
        {
            return new Medicamento("Dipirona", "descrição dipirona", "123", new DateTime(2022, 10, 10, 22, 35, 5), 5);
        }
        #endregion
    }
}
