using System;
using ControleMedicamentos.Dominio.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloPaciente
{
    [TestClass]
    public class PacienteTest
    {
        [TestMethod]
        public void Atualizar_nome()
        {
            Paciente paciente = new Paciente();
            paciente.Nome = "Nome";

            Paciente pacienteAtualizado = new Paciente();
            pacienteAtualizado.Nome = "Nome_atualizado";

            paciente.AtualizarRegistro(pacienteAtualizado);

            Assert.AreEqual("Nome_atualizado", paciente.Nome);
        }

        [TestMethod]
        public void Atualizar_cartao_sus()
        {
            Paciente paciente = new Paciente();
            paciente.CartaoSUS = "Sus123";

            Paciente pacienteAtualizado = new Paciente();
            pacienteAtualizado.CartaoSUS = "Sus123_atualizado";

            paciente.AtualizarRegistro(pacienteAtualizado);

            Assert.AreEqual("Sus123_atualizado", paciente.CartaoSUS);
        }

        [TestMethod]
        public void Contrutor_vazio()
        {
            Paciente paciente = new Paciente();

            Assert.AreEqual(null, paciente.Nome);
            Assert.AreEqual(null, paciente.CartaoSUS);
        }

        [TestMethod]
        public void Contrutor_todos_parametros()
        {
            Paciente paciente = new Paciente("Nome", "123");

            Assert.AreEqual("Nome", paciente.Nome);
            Assert.AreEqual("123", paciente.CartaoSUS);
        }
    }
}
