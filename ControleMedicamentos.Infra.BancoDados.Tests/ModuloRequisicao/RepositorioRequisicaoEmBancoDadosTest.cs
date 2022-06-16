using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using System.Collections.Generic;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleRequisicaos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloRequisicao
{
    [TestClass]
    public class RepositorioRequisicaoEmBancoDadosTest
    {
        public RepositorioRequisicaoEmBancoDadosTest()
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
        public void Deve_inserir_requisicao()
        {
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();
            IRepositorioRequisicao repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

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
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();
            IRepositorioRequisicao repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

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
            Requisicao requisicaoAlterada = ObterRequisicao2();

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
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();
            IRepositorioRequisicao repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

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

            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();
            IRepositorioRequisicao repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            Requisicao requisicao1 = ObterRequisicao();
            Requisicao requisicao2 = ObterRequisicao2();

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
            IRepositorioMedicamento repositorioMedicamento = new RepositorioMedicamentoEmBancoDados();
            IRepositorioFornecedor repositorioFornecedor = new RepositorioFornecedor();
            IRepositorioFuncionario repositorioFuncionario = new RepositorioFuncionario();
            IRepositorioPaciente repositorioPaciente = new RepositorioPaciente();
            IRepositorioRequisicao repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();

            Requisicao requisicao1 = ObterRequisicao();
            Requisicao requisicao2 = ObterRequisicao2();

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
        private Requisicao ObterRequisicao()
        {
            Paciente paciente = new Paciente("Paciente 1", "123456789123456");
            Fornecedor fornecedor = new Fornecedor("Nome fornecedor", "55 49 9999-0000", "teste@teste.com", "cidade", "estado");
            Funcionario funcionario = new Funcionario("Nome funcionario", "admin1", "admin1");
            Medicamento medicamento = new Medicamento("Nome medicamento", "descricao medicamento", "123", new DateTime(2022, 10, 10), 4);
            medicamento.Fornecedor = fornecedor;
            Requisicao requisicao = new Requisicao(medicamento, paciente, 3, new DateTime(2022, 10, 10), funcionario);
            

            return requisicao;
        }
        private Requisicao ObterRequisicao2()
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
