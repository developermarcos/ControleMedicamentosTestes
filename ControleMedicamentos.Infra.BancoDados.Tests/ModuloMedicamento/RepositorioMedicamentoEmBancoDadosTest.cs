using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;

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

            Medicamento medicamentoEditado = new Medicamento("Medicamento editado", "Descrição medicamento editado", "123", new DateTime(2022, 11, 10), 4);

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

        [TestMethod]
        public void Deve_selecionar_todas_requisicoes_de_um_medicamento()
        {
            Requisicao requisicao1 = ObterRequisicao();
            requisicao1.Data = new DateTime(2022, 06, 20);
            repositorioFornecedor.Inserir(requisicao1.Medicamento.Fornecedor);
            repositorioFuncionario.Inserir(requisicao1.Funcionario);
            repositorioPaciente.Inserir(requisicao1.Paciente);
            repositorioMedicamento.Inserir(requisicao1.Medicamento);
            repositorioRequisicao.Inserir(requisicao1);

            requisicao1.Data = new DateTime(2022, 09, 23);
            repositorioRequisicao.Inserir(requisicao1);

            requisicao1.Data = new DateTime(2022, 07, 21);
            requisicao1.Medicamento.Nome = "Medicamento 2";

            repositorioMedicamento.Inserir(requisicao1.Medicamento);
            repositorioRequisicao.Inserir(requisicao1);


            requisicao1.Data = new DateTime(2022, 08, 22);
            requisicao1.Medicamento.Nome = "Medicamento 3";

            repositorioMedicamento.Inserir(requisicao1.Medicamento);
            repositorioRequisicao.Inserir(requisicao1);


            var medicamentoBuscar = ObterMedicamento();
            medicamentoBuscar.Id = 1;

            var medicamento = repositorioMedicamento.SelecionarPorId(medicamentoBuscar);

            var medicamentoComRequisicoes = repositorioMedicamento.SelecionarRequisicoesMedicamento(medicamento);

            Assert.AreEqual(2, medicamentoComRequisicoes.QuantidadeRequisicoes);
        }
    }
}