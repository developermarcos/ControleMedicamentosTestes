using System.Collections.Generic;
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloFornecedor
{
    [TestClass]
    public class RepositorioFornecedorTest : BaseTest
    {
        [TestMethod]
        public void Deve_inserir_fornecedor()
        {
            var validationResult = repositorioFornecedor.Inserir(ObterFornecedor());

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            Fornecedor fornecedor = ObterFornecedor();

            repositorioFornecedor.Inserir(fornecedor);

            fornecedor.Nome= "Funcuinario nome Editar";

            var validationResult = repositorioFornecedor.Editar(fornecedor);
            
            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            Fornecedor fornecedor = ObterFornecedor();
            
            repositorioFornecedor.Inserir(fornecedor);

            var validationResult = repositorioFornecedor.Excluir(fornecedor);
            
            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_selecionar_todos_registros_de_fornecedor()
        {
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Fornecedor fornecedorInserir1 = ObterFornecedor();
            
            Fornecedor fornecedorInserir2 = ObterFornecedor();
            fornecedorInserir2.Nome = "Fornecedor 2";

            Fornecedor fornecedorInserir3 = ObterFornecedor();
            fornecedorInserir3.Nome = "Fornecedor 3";

            List<Fornecedor> funcionariosInserir = new List<Fornecedor> 
            {
                fornecedorInserir1,
                fornecedorInserir2,
                fornecedorInserir3
            };
            
            foreach (var item in funcionariosInserir)
                repositorioFornecedor.Inserir(item);
            
            var todosFuncionarios = repositorioFornecedor.SelecionarTodos();

            Assert.AreEqual(3, todosFuncionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_funcionario_por_id()
        {
            Fornecedor fornecedorInserir = ObterFornecedor();

            repositorioFornecedor.Inserir(fornecedorInserir);

            Fornecedor fornecedorPesquisa = repositorioFornecedor.SelecionarPorId(fornecedorInserir);

            Assert.AreEqual(true, fornecedorPesquisa.Equals(fornecedorInserir));
        }
    }
}