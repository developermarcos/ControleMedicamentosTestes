using System;
using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class ValidadorPacienteTest
    {
        [TestMethod]
        public void Nome_paciente_nao_deve_ser_nulo()
        {
            Paciente paciente = new Paciente();

            paciente.Nome = null;
            paciente.CartaoSUS = "Login1";
            
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            Assert.AreEqual("Campo 'Nome' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_paciente_nao_pode_ser_vazio()
        {
            var paciente = new Paciente();

            paciente.Nome = "";
            paciente.CartaoSUS = "Login1";
            
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            Assert.AreEqual("Campo 'Nome' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSus_paciente_nao_deve_ser_nulo()
        {
            Paciente paciente = new Paciente();

            paciente.Nome = "Nome teste";
            paciente.CartaoSUS = null;

            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            Assert.AreEqual("Campo 'CartaoSUS' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void CartaoSus_paciente_nao_pode_ser_vazio()
        {
            var paciente = new Paciente();

            paciente.Nome = "Nome teste";
            paciente.CartaoSUS = "";

            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            Assert.AreEqual("Campo 'CartaoSUS' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }
    }
}
