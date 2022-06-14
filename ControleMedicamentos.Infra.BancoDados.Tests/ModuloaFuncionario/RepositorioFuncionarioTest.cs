using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloaFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioTest
    {
        public RepositorioFuncionarioTest()
        {
            string sql =
                @"DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)";

            Db.ExecutarSql(sql);
        }

        [TestMethod]
        public void Deve_inserir_funcionario()
        {
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();

            Funcionario funcionario = ObterObjetoFuncionario();

            var validationResult = repositorioFuncionario.Inserir(funcionario);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        
        [TestMethod]
        public void Deve_Editar_funcionario()
        {
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();

            Funcionario funcionario = ObterObjetoFuncionario(); ;

            var validationResult = repositorioFuncionario.Inserir(funcionario);

            if (validationResult.IsValid)
            {
                funcionario.Nome= "Marcos Lima Editar";

                repositorioFuncionario.Editar(funcionario);
            }

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();

            Funcionario funcionario = ObterObjetoFuncionario();

            var validationResult = repositorioFuncionario.Inserir(funcionario);

            if (validationResult.IsValid)
            {
                repositorioFuncionario.Excluir(funcionario);
            }

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_selecionar_todos_registros_de_funcionario()
        {
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();

            Funcionario funcionario = ObterObjetoFuncionario();
            Funcionario funcionario2 = ObterObjetoFuncionario();
            Funcionario funcionario3 = ObterObjetoFuncionario();

            funcionario2.Nome = "Funcionario 2";
            funcionario3.Nome = "Funcionario 3";

            List<Funcionario> funcionariosInserir = new List<Funcionario>();
            funcionariosInserir.Add(funcionario);
            funcionariosInserir.Add(funcionario2);
            funcionariosInserir.Add(funcionario3);

            foreach (var item in funcionariosInserir)
            {
                repositorioFuncionario.Inserir(item);
            }

            var todosFuncionarios = repositorioFuncionario.SelecionarTodos();

            Assert.AreEqual(3, todosFuncionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_funcionario_por_id()
        {
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();

            Funcionario funcionarioInserir = ObterObjetoFuncionario();

            repositorioFuncionario.Inserir(funcionarioInserir);

            Funcionario funcionarioPesquisa = repositorioFuncionario.SelecionarPorId(funcionarioInserir);

            Assert.AreEqual(true, funcionarioPesquisa.Equals(funcionarioInserir));
        }

        [TestMethod]
        public void Nao_deve_inserir_funcionario_nome_repetido()
        {
            //IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();

            //Funcionario funcionario = new Funcionario("Marcos", "admin1", "Admin1");

            //var validationResult = repositorioFuncionario.Inserir(funcionario);

            //Assert.AreEqual(true, validationResult.IsValid);
        }

        #region Métodos privados
        private static Funcionario ObterObjetoFuncionario()
        {
            return new Funcionario("Funcionario Inserrir", "admin1", "Admin1");
        }
        #endregion
    }
}
