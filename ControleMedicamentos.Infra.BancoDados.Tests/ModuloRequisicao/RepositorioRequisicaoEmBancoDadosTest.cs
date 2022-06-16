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
        public void Deve_inserir_medicamento()
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
        #endregion
    }
}
