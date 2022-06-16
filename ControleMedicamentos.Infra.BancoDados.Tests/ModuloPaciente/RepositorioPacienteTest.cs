using System.Collections.Generic;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteTest : BaseTest
    {
        [TestMethod]
        public void Deve_inserir_paciente()
        {
            var validationResult = repositorioPaciente.Inserir(ObterPaciente());

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_editar_paciente()
        {
            Paciente paciente = ObterPaciente();

            repositorioPaciente.Inserir(paciente);

            paciente.Nome = "Paciente 1 atualizado";

            var validationResult = repositorioPaciente.Editar(paciente);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_excluir_paciente()
        {
            Paciente paciente = ObterPaciente();

            repositorioPaciente.Inserir(paciente);

            var validationResult = repositorioPaciente.Excluir(paciente);
            
            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_selecionar_todos_registros_de_paciente()
        {
            Paciente pacienteInserir1 = ObterPaciente();
            
            Paciente pacienteInserir2 = ObterPaciente();
            pacienteInserir2.Nome = "Paciente 2";

            Paciente pacienteInserir3 = ObterPaciente();
            pacienteInserir3.Nome = "Paciente 3";

            List<Paciente> funcionariosInserir = new List<Paciente>
            {
                pacienteInserir1,
                pacienteInserir2,
                pacienteInserir3
            };
            
            foreach (var item in funcionariosInserir)
                repositorioPaciente.Inserir(item);

            var todosFuncionarios = repositorioPaciente.SelecionarTodos();

            Assert.AreEqual(3, todosFuncionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_paciente_por_id()
        {
            Paciente pacienteInserir = ObterPaciente();

            repositorioPaciente.Inserir(pacienteInserir);

            Paciente pacientePesquisa = repositorioPaciente.SelecionarPorId(pacienteInserir);

            Assert.AreEqual(true, pacientePesquisa.Equals(pacienteInserir));
        }
    }
}
