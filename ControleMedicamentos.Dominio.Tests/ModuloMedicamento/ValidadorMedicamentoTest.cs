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

        [TestMethod]
        public void Nome_medicamento_nao_deve_ser_vazio()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            medicamento.Nome = "";

            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("Campo 'Nome' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_medicamento_deve_conter_minimo_6_digitos()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            medicamento.Nome = "abcde";

            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("Campo 'Nome' deve conter pelo menos 6 digitos.", resultadoValidacao.Errors[0].ErrorMessage);
        }


        [TestMethod]
        public void Descricao_medicamento_nao_deve_ser_nulo()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            medicamento.Descricao = null;

            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("Campo 'Descricao' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_medicamento_nao_deve_ser_vazio()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            medicamento.Descricao = "";

            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("Campo 'Descricao' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Descricao_medicamento_deve_conter_minimo_6_digitos()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            medicamento.Descricao = "abcde";

            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual("Campo 'Descricao' deve conter pelo menos 10 digitos.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        
        [TestMethod]
        public void QuantidadeDisponivel_medicamento_nao_deve_ser_menor_zero()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            medicamento.BaixarMedicamento(6);

            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            Assert.AreEqual(true, resultadoValidacao.IsValid);
        }

        #region Métodos privados
        private static Medicamento CriaObjetoMedicamento()
        {
            return new Medicamento("Dipirona", "descrição dipirona", "123", new DateTime(2022, 10, 10, 22, 35, 5), 5);
        }
        #endregion
    }
}
