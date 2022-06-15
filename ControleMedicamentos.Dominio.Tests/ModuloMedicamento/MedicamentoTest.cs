using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]
        public void AtualizarNome()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Medicamento medicamentoAtualizado = medicamento.Clonar();

            medicamentoAtualizado.Nome = "Nome_atualizado";

            medicamento.AtualizarRegistro(medicamentoAtualizado);

            Assert.AreEqual(medicamento.Nome, medicamentoAtualizado.Nome);
        }

        [TestMethod]
        public void AtualizarDescricao()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Medicamento medicamentoAtualizado = medicamento.Clonar();

            medicamentoAtualizado.Descricao = "Descricao_atualizado";

            medicamento.AtualizarRegistro(medicamentoAtualizado);

            Assert.AreEqual(medicamento.Descricao, medicamentoAtualizado.Descricao);
        }

        [TestMethod]
        public void AtualizarLote()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Medicamento medicamentoAtualizado = medicamento.Clonar();

            medicamentoAtualizado.Lote = "Lote_atualizado";

            medicamento.AtualizarRegistro(medicamentoAtualizado);

            Assert.AreEqual(medicamento.Lote, medicamentoAtualizado.Lote);
        }

        [TestMethod]
        public void AtualizarValidade()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Medicamento medicamentoAtualizado = medicamento.Clonar();

            medicamentoAtualizado.Validade = new DateTime(2022, 11, 10, 22, 35, 5);

            medicamento.AtualizarRegistro(medicamentoAtualizado);

            Assert.AreEqual(medicamentoAtualizado.Validade, medicamento.Validade);
        }

        [TestMethod]
        public void AtualizarFornecedor()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Medicamento medicamentoAtualizado = medicamento.Clonar();

            medicamentoAtualizado.Fornecedor = new Fornecedor("Nome", "123", "Email", "Cidade", "Estado");

            medicamento.Fornecedor.AtualizarRegistro(medicamentoAtualizado.Fornecedor);

            Assert.AreEqual(true, medicamentoAtualizado.Fornecedor.Equals(medicamento.Fornecedor));
        }

        #region Valida requisições
        [TestMethod]
        public void Deve_adicionar_requisicoes_no_medicamento()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Paciente paciente1 = new Paciente("Joao da silva1", "123456789123456");
            Paciente paciente2 = new Paciente("Joao da silva2", "123456789123456");
            Paciente paciente3 = new Paciente("Joao da silva3", "123456789123456");
            Funcionario funcionario1 = new Funcionario("Nome funcionario1", "admin1", "admin1");
            Funcionario funcionario2 = new Funcionario("Nome funcionario2", "admin2", "admin2");
            Funcionario funcionario3 = new Funcionario("Nome funcionario3", "admin3", "admin3");

            Requisicao requisicao1 = new Requisicao(medicamento, paciente1, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario1);
            Requisicao requisicao2 = new Requisicao(medicamento, paciente2, 6, new DateTime(2022, 10, 10, 05, 05, 05), funcionario2);
            Requisicao requisicao3 = new Requisicao(medicamento, paciente3, 7, new DateTime(2022, 10, 10, 05, 05, 05), funcionario3);

            medicamento.AdicionarRequisicao(requisicao1);
            medicamento.AdicionarRequisicao(requisicao2);
            medicamento.AdicionarRequisicao(requisicao3);

            Assert.AreEqual(3, medicamento.QuantidadeRequisicoes);
        }

        [TestMethod]
        public void Nao_deve_adicionar_requisicoes_repetidas()
        {
            Medicamento medicamento = CriaObjetoMedicamento();
            Paciente paciente1 = new Paciente("Joao da silva1", "123456789123456");
            Paciente paciente2 = new Paciente("Joao da silva2", "123456789123456");
            Funcionario funcionario1 = new Funcionario("Nome funcionario1", "admin", "admin");
            Funcionario funcionario2 = new Funcionario("Nome funcionario2", "admin", "admin");

            Requisicao requisicao1 = new Requisicao(medicamento, paciente1, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario1);
            Requisicao requisicao2 = new Requisicao(medicamento, paciente2, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario2);
            Requisicao requisicao3 = new Requisicao(medicamento, paciente1, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario1);
            Requisicao requisicao4 = new Requisicao(medicamento, paciente2, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario2);

            medicamento.AdicionarRequisicao(requisicao1);
            medicamento.AdicionarRequisicao(requisicao2);
            medicamento.AdicionarRequisicao(requisicao3);
            medicamento.AdicionarRequisicao(requisicao4);

            Assert.AreEqual(2, medicamento.QuantidadeRequisicoes);
        }

        [TestMethod]
        public void Deve_baixar_quantidade_estoque()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Paciente paciente = new Paciente("Joao da silva", "123456789123456");

            Funcionario funcionario = new Funcionario("Nome funcionario", "admin", "admin");

            Requisicao requisicao1 = new Requisicao(medicamento, paciente, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario);

            medicamento.AdicionarRequisicao(requisicao1);

            medicamento.BaixarMedicamento(4);

            Assert.AreEqual(1, medicamento.QuantidadeDisponivel);
        }

        [TestMethod]
        public void Estoque_nao_deve_ser_negativo()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Paciente paciente = new Paciente("Joao da silva", "123456789123456");

            Funcionario funcionario = new Funcionario("Nome funcionario", "admin", "admin");

            Requisicao requisicao1 = new Requisicao(medicamento, paciente, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario);
            
            medicamento.AdicionarRequisicao(requisicao1);

            medicamento.BaixarMedicamento(6);
            
            Assert.AreEqual(5, medicamento.QuantidadeDisponivel);
        }

        #endregion

        #region Métodos privados
        private static Medicamento CriaObjetoMedicamento()
        {
            return new Medicamento("Dipirona", "descrição dipirona", "123", new DateTime(2022, 10, 10, 22, 35, 5), 5);
        }
        #endregion
    }
}
