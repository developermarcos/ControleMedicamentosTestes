using System;
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
            string sql =
                @"DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)";

            Db.ExecutarSql(sql);
        }
        [TestMethod]
        public void Deve_inserir_fornecedor()
        {
            IRepositorioFornecedor repositorioFuncionario = new RepositorioFornecedor();

            Fornecedor fornecedor = ObterObjetoFornecedor();

            var validationResult = repositorioFuncionario.Inserir(fornecedor);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        #region Métodos privados
        private Fornecedor ObterObjetoFornecedor()
        {
            return new Fornecedor("Funcionario Inserir", "123456", "teste@teste.com", "Cidade", "Estado");
        }
        #endregion
    }
}
