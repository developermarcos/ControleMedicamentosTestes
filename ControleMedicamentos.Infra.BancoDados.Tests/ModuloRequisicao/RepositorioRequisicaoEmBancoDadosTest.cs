using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleFornecedors.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest : BaseTest
    {
        [TestMethod]
        public void Deve_inserir_requisicao()
        {
            Requisicao requisicao = ObterRequisicao();

            repositorioPaciente.Inserir(requisicao.Paciente);
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);
            repositorioFuncionario.Inserir(requisicao.Funcionario);
            repositorioMedicamento.Inserir(requisicao.Medicamento);
            
            var validationResult = repositorioRequisicao.Inserir(requisicao);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_editar_requisicao()
        {
            Requisicao requisicao = ObterRequisicao();

            repositorioPaciente.Inserir(requisicao.Paciente);
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);
            repositorioFuncionario.Inserir(requisicao.Funcionario);
            repositorioMedicamento.Inserir(requisicao.Medicamento);

            var validationResult = repositorioRequisicao.Inserir(requisicao);
            
            if (!validationResult.IsValid)
            {
                Assert.Fail("Erro ao inserir fornecedor");
                return;
            }
            Requisicao requisicaoAlterada = ObterRequisicaoAlterada();

            requisicao.AtualizarRequisicao(requisicaoAlterada);

            repositorioPaciente.Inserir(requisicao.Paciente);
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);
            repositorioFuncionario.Inserir(requisicao.Funcionario);
            repositorioMedicamento.Inserir(requisicao.Medicamento);

            
            validationResult = repositorioRequisicao.Editar(requisicao);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_excluir_requisicao()
        {
            Requisicao requisicao = ObterRequisicao();

            repositorioPaciente.Inserir(requisicao.Paciente);
            repositorioFornecedor.Inserir(requisicao.Medicamento.Fornecedor);
            repositorioFuncionario.Inserir(requisicao.Funcionario);
            repositorioMedicamento.Inserir(requisicao.Medicamento);

            var validationResult = repositorioRequisicao.Inserir(requisicao);

            if (!validationResult.IsValid)
            {
                Assert.Fail("Erro ao inserir fornecedor");
                return;
            }

            validationResult = repositorioRequisicao.Excluir(requisicao);

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_selecionar_todos_registros_de_requisicao()
        {
            Requisicao requisicao1 = ObterRequisicao();
            Requisicao requisicao2 = ObterRequisicaoAlterada();

            List<Paciente> pacientes = new List<Paciente>
            {
                requisicao1.Paciente,
                requisicao2.Paciente
            };
            List<Fornecedor> fornecedores = new List<Fornecedor>
            {
                requisicao1.Medicamento.Fornecedor,
                requisicao2.Medicamento.Fornecedor
            };
            List<Funcionario> funcionarios = new List<Funcionario>
            {
                requisicao1.Funcionario,
                requisicao2.Funcionario
            };
            List<Medicamento> medicamentos = new List<Medicamento>
            {
                requisicao1.Medicamento,
                requisicao2.Medicamento
            };
            List<Requisicao> requisicoes = new List<Requisicao>
            {
                requisicao1,
                requisicao2
            };

            
            foreach(var item in pacientes)
                repositorioPaciente.Inserir(item);
            
            foreach (var item in fornecedores)
                repositorioFornecedor.Inserir(item);
            
            foreach (var item in funcionarios)
                repositorioFuncionario.Inserir(item);
            
            foreach (var item in medicamentos)
                repositorioMedicamento.Inserir(item);
            
            foreach (var item in requisicoes)
                repositorioRequisicao.Inserir(item);
            


            var todosRequisicoes = repositorioFornecedor.SelecionarTodos();

            Assert.AreEqual(2, todosRequisicoes.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_requisicao_por_id()
        {
            Requisicao requisicao1 = ObterRequisicao();
            Requisicao requisicao2 = ObterRequisicaoAlterada();

            List<Paciente> pacientes = new List<Paciente>();
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            List<Funcionario> funcionarios = new List<Funcionario>();
            List<Medicamento> medicamentos = new List<Medicamento>();
            List<Requisicao> requisicoes = new List<Requisicao>();

            pacientes.Add(requisicao1.Paciente);
            pacientes.Add(requisicao2.Paciente);

            fornecedores.Add(requisicao1.Medicamento.Fornecedor);
            fornecedores.Add(requisicao2.Medicamento.Fornecedor);

            funcionarios.Add(requisicao1.Funcionario);
            funcionarios.Add(requisicao2.Funcionario);

            medicamentos.Add(requisicao1.Medicamento);
            medicamentos.Add(requisicao2.Medicamento);

            requisicoes.Add(requisicao1);
            requisicoes.Add(requisicao2);

            foreach (var item in pacientes)
                repositorioPaciente.Inserir(item);

            foreach (var item in fornecedores)
                repositorioFornecedor.Inserir(item);

            foreach (var item in funcionarios)
                repositorioFuncionario.Inserir(item);

            foreach (var item in medicamentos)
                repositorioMedicamento.Inserir(item);

            foreach (var item in requisicoes)
                repositorioRequisicao.Inserir(item);

            var requisicaoPorId = repositorioRequisicao.SelecionarPorId(requisicao1);

            Assert.AreEqual(true, requisicao1.Equals(requisicaoPorId));
        }

        #region MÉTODOS PRIVADOS

        private Requisicao ObterRequisicaoAlterada()
        {
            Paciente paciente = new Paciente("2 Paciente", "123456789123456");
            Fornecedor fornecedor = new Fornecedor("2 Nome fornecedor", "55 49 9999-0000", "teste@teste.com", "cidade", "estado");
            Funcionario funcionario = new Funcionario("2 Nome funcionario", "2admin1", "admin1");
            Medicamento medicamento = new Medicamento("2 Nome medicamento", "2descricao medicamento", "123", new DateTime(2022, 10, 10), 4);
            medicamento.Fornecedor = fornecedor;
            Requisicao requisicao = new Requisicao(medicamento, paciente, 4, new DateTime(2022, 10, 20), funcionario);

            return requisicao;
        }

        #endregion
        }
    }
