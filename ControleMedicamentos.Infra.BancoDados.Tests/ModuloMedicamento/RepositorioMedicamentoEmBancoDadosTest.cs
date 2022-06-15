using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using System.Collections.Generic;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest
    {
        public RepositorioMedicamentoEmBancoDadosTest()
        {
            string sql =
                @"DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)";

            Db.ExecutarSql(sql);

            string sql2 =
                @"DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)";

            Db.ExecutarSql(sql2);
        }
        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();
            
            Medicamento medicamento = ObterObjetoMedicamento();

            var resultado = repositorioFornecedor.Inserir(medicamento.Fornecedor);

            if (!resultado.IsValid)
            {
                Assert.Fail("Erro ao inserir fornecedor");
                return;
            }
            
            var validationResult = repositorioMedicamento.Inserir(medicamento);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Medicamento medicamento = ObterObjetoMedicamento();

            var resultado = repositorioFornecedor.Inserir(medicamento.Fornecedor);

            if (!resultado.IsValid)
            {
                Assert.Fail("Erro ao inserir fornecedor");
                return;
            }

            var validationResult = repositorioMedicamento.Inserir(medicamento);

            Fornecedor Fornecedor = new Fornecedor("Funcionario Inserir", "1234567891234565", "teste@teste.com", "Cidade", "Estado");
            Medicamento medicamentoEditado = new Medicamento ("Medicamento editado", "Descrição medicamento editado", "123", new DateTime(2022, 11, 10), 4);
            medicamentoEditado.Fornecedor = Fornecedor;

            medicamento.AtualizarRegistro(medicamentoEditado);

            validationResult = repositorioMedicamento.Editar(medicamento);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_selecionar_todos_registros_de_medicamento()
        {
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Medicamento medicamentoInserir1 = ObterObjetoMedicamento();
            Medicamento medicamentoInserir2 = ObterObjetoMedicamento();
            Medicamento medicamentoInserir3 = ObterObjetoMedicamento();

            medicamentoInserir2.Nome = "medicamento 2";
            medicamentoInserir3.Nome = "medicamento 3";

            List<Medicamento> funcionariosInserir = new List<Medicamento>();

            funcionariosInserir.Add(medicamentoInserir1);
            funcionariosInserir.Add(medicamentoInserir2);
            funcionariosInserir.Add(medicamentoInserir3);

            foreach (var item in funcionariosInserir)
            {
                repositorioFornecedor.Inserir(item.Fornecedor);
            }

            foreach (var item in funcionariosInserir)
            {
                repositorioMedicamento.Inserir(item);
            }

            var todosFuncionarios = repositorioFornecedor.SelecionarTodos();

            Assert.AreEqual(3, todosFuncionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_medicamento_por_id()
        {
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Medicamento medicamentoInserir = ObterObjetoMedicamento();

            repositorioFornecedor.Inserir(medicamentoInserir.Fornecedor);

            repositorioMedicamento.Inserir(medicamentoInserir);

            Medicamento medicamentoPesquisa = repositorioMedicamento.SelecionarPorId(medicamentoInserir);

            Assert.AreEqual(true, medicamentoPesquisa.Equals(medicamentoInserir));
        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();

            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();

            Medicamento medicamentoInserir = ObterObjetoMedicamento();

            repositorioFornecedor.Inserir(medicamentoInserir.Fornecedor);

            var validationResult = repositorioMedicamento.Inserir(medicamentoInserir);

            if (validationResult.IsValid)
            {
                validationResult = repositorioMedicamento.Excluir(medicamentoInserir);
            }

            Assert.AreEqual(true, validationResult.IsValid);
        }

        private Medicamento ObterObjetoMedicamento()
        {
            Fornecedor Fornecedor = new Fornecedor("Funcionario Inserir", "1234567891234565", "teste@teste.com", "Cidade", "Estado");

            Medicamento medicamento = new Medicamento("Medicamento Inserir", "Descricao inserir", "123", new DateTime(2022, 11, 10), 4);
            
            medicamento.Fornecedor = Fornecedor;

            return medicamento;
        }
    }
}
