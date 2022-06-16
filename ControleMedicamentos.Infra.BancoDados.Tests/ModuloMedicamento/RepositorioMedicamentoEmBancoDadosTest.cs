using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;

namespace ControleMedicamento.Infra.BancoDados.Tests.ModuloMedicamento
{
    [TestClass]
    public class RepositorioMedicamentoEmBancoDadosTest : BaseTest
    {
        [TestMethod]
        public void Deve_inserir_medicamento()
        {
            var medicamento = ObterMedicamento();

            repositorioFornecedor.Inserir(medicamento.Fornecedor);

            var validationResult = repositorioMedicamento.Inserir(medicamento);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_editar_fornecedor()
        {
            Medicamento medicamento = ObterMedicamento();

            repositorioFornecedor.Inserir(medicamento.Fornecedor);

            repositorioMedicamento.Inserir(medicamento);

            Medicamento medicamentoEditado = new Medicamento("Medicamento editado", "Descri��o medicamento editado", "123", new DateTime(2022, 11, 10), 4);

            medicamentoEditado.Fornecedor = ObterFornecedor();

            medicamento.AtualizarRegistro(medicamentoEditado);

            var validationResult = repositorioMedicamento.Editar(medicamento);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_selecionar_todos_registros_de_medicamento()
        {
            Medicamento medicamentoInserir1 = ObterMedicamento();
            
            Medicamento medicamentoInserir2 = ObterMedicamento();
            medicamentoInserir2.Nome = "medicamento 2";
            
            Medicamento medicamentoInserir3 = ObterMedicamento();
            medicamentoInserir3.Nome = "medicamento 3";

            List<Medicamento> funcionariosInserir = new List<Medicamento> 
            { 
                medicamentoInserir1, 
                medicamentoInserir2, 
                medicamentoInserir3
            };

            foreach (var item in funcionariosInserir)
                repositorioFornecedor.Inserir(item.Fornecedor);
            
            foreach (var item in funcionariosInserir)
                repositorioMedicamento.Inserir(item);

            var todosFuncionarios = repositorioFornecedor.SelecionarTodos();

            Assert.AreEqual(3, todosFuncionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_medicamento_por_id()
        {
            Medicamento medicamentoInserir = ObterMedicamento();

            repositorioFornecedor.Inserir(medicamentoInserir.Fornecedor);

            repositorioMedicamento.Inserir(medicamentoInserir);

            Medicamento medicamentoPesquisa = repositorioMedicamento.SelecionarPorId(medicamentoInserir);

            Assert.AreEqual(true, medicamentoPesquisa.Equals(medicamentoInserir));
        }

        [TestMethod]
        public void Deve_excluir_fornecedor()
        {
            Medicamento medicamentoInserir = ObterMedicamento();

            repositorioFornecedor.Inserir(medicamentoInserir.Fornecedor);

            var validationResult = repositorioMedicamento.Inserir(medicamentoInserir);

            if (validationResult.IsValid)
            {
                validationResult = repositorioMedicamento.Excluir(medicamentoInserir);
            }

            Assert.AreEqual(true, validationResult.IsValid);
        }
    }
}