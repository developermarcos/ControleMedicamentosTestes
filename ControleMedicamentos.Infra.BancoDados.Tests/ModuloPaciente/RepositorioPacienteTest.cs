using System;
using System.Collections.Generic;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloPaciente
{
    [TestClass]
    public class RepositorioPacienteTest
    {
        public RepositorioPacienteTest()
        {
            string sql1 =
                @"DELETE FROM TBREQUISICAO;
                  DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)";

            Db.ExecutarSql(sql1);

            string sql2 =
                @"DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)";

            Db.ExecutarSql(sql2);

            string sql3 =
                @"DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)";

            Db.ExecutarSql(sql3);

            string sql4 =
                @"DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)";

            Db.ExecutarSql(sql4);

            string sql5 =
                @"DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)";

            Db.ExecutarSql(sql5);
        }
        [TestMethod]
        public void Deve_inserir_paciente()
        {
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();

            Paciente paciente = ObterObjetoPaciente();

            var validationResult = repositorioPaciente.Inserir(paciente);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_editar_paciente()
        {
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();

            Paciente paciente = ObterObjetoPaciente();

            var validationResult = repositorioPaciente.Inserir(paciente);

            paciente.Nome = "Paciente 1 atualizado";

            validationResult = repositorioPaciente.Editar(paciente);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_excluir_paciente()
        {
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();

            Paciente paciente = ObterObjetoPaciente();

            var validationResult = repositorioPaciente.Inserir(paciente);

            if (validationResult.IsValid)
            {
                validationResult = repositorioPaciente.Excluir(paciente);
            }

            Assert.AreEqual(true, validationResult.IsValid);
        }
        [TestMethod]
        public void Deve_selecionar_todos_registros_de_paciente()
        {
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();

            Paciente pacienteInserir1 = ObterObjetoPaciente();
            Paciente pacienteInserir2 = ObterObjetoPaciente();
            Paciente pacienteInserir3 = ObterObjetoPaciente();

            pacienteInserir2.Nome = "Paciente 2";
            pacienteInserir3.Nome = "Paciente 3";

            List<Paciente> funcionariosInserir = new List<Paciente>();
            funcionariosInserir.Add(pacienteInserir1);
            funcionariosInserir.Add(pacienteInserir1);
            funcionariosInserir.Add(pacienteInserir1);

            foreach (var item in funcionariosInserir)
            {
                repositorioPaciente.Inserir(item);
            }

            var todosFuncionarios = repositorioPaciente.SelecionarTodos();

            Assert.AreEqual(3, todosFuncionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_paciente_por_id()
        {
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();

            Paciente pacienteInserir1 = ObterObjetoPaciente();

            repositorioPaciente.Inserir(pacienteInserir1);

            Paciente pacientePesquisa = repositorioPaciente.SelecionarPorId(pacienteInserir1);

            Assert.AreEqual(true, pacientePesquisa.Equals(pacienteInserir1));
        }

        #region métodos privados
        private Paciente ObterObjetoPaciente()
        {
            return new Paciente("Paciente 1", "123456789123456");
        }
        #endregion
    }
}
