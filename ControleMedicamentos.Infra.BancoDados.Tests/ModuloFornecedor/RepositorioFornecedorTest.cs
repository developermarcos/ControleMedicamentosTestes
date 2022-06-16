using System;
using System.Collections.Generic;
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorTest
    {
        public RepositorioFornecedorTest()
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
        public void Deve_inserir_fornecedor()
        {
            IRepositorioFornecedor repositorioFuncionario = new RepositorioFornecedor();

            Fornecedor fornecedor = ObterObjetoFornecedor();

            var validationResult = repositorioFuncionario.Inserir(fornecedor);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Fornecedor fornecedor = ObterObjetoFornecedor();

            var validationResult = repositorioFornecedor.Inserir(fornecedor);

            if (validationResult.IsValid)
            {
                fornecedor.Nome= "Funcuinario nome Editar";

                repositorioFornecedor.Editar(fornecedor);
            }

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Fornecedor fornecedor = ObterObjetoFornecedor();
            
            var validationResult = repositorioFornecedor.Inserir(fornecedor);

            if (validationResult.IsValid)
            {
                validationResult = repositorioFornecedor.Excluir(fornecedor);
            }

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_selecionar_todos_registros_de_fornecedor()
        {
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Fornecedor fornecedorInserir1 = ObterObjetoFornecedor();
            Fornecedor fornecedorInserir2 = ObterObjetoFornecedor();
            Fornecedor fornecedorInserir3 = ObterObjetoFornecedor();

            fornecedorInserir2.Nome = "Fornecedor 2";
            fornecedorInserir3.Nome = "Fornecedor 3";

            List<Fornecedor> funcionariosInserir = new List<Fornecedor>();
            funcionariosInserir.Add(fornecedorInserir1);
            funcionariosInserir.Add(fornecedorInserir2);
            funcionariosInserir.Add(fornecedorInserir3);

            foreach (var item in funcionariosInserir)
            {
                repositorioFornecedor.Inserir(item);
            }

            var todosFuncionarios = repositorioFornecedor.SelecionarTodos();

            Assert.AreEqual(3, todosFuncionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_funcionario_por_id()
        {
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Fornecedor fornecedorInserir = ObterObjetoFornecedor();

            repositorioFornecedor.Inserir(fornecedorInserir);

            Fornecedor fornecedorPesquisa = repositorioFornecedor.SelecionarPorId(fornecedorInserir);

            Assert.AreEqual(true, fornecedorPesquisa.Equals(fornecedorInserir));
        }

        #region Métodos privados
        private Fornecedor ObterObjetoFornecedor()
        {
            return new Fornecedor("Funcionario Inserir", "1234567891234565", "teste@teste.com", "Cidade", "Estado");
        }
        #endregion
    }
}
