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

            VerificaObjetosIguais(medicamento, medicamentoAtualizado);
        }

        [TestMethod]
        public void AtualizarDescricao()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Medicamento medicamentoAtualizado = medicamento.Clonar();

            medicamentoAtualizado.Descricao = "Descricao_atualizado";

            VerificaObjetosIguais(medicamento, medicamentoAtualizado);
        }

        [TestMethod]
        public void AtualizarLote()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Medicamento medicamentoAtualizado = medicamento.Clonar();

            medicamentoAtualizado.Lote = "Lote_atualizado";

            VerificaObjetosIguais(medicamento, medicamentoAtualizado);
        }

        [TestMethod]
        public void AtualizarValidade()
        {
            Medicamento medicamento = CriaObjetoMedicamento();

            Medicamento medicamentoAtualizado = medicamento.Clonar();

            medicamentoAtualizado.Validade = new DateTime(2022, 11, 10, 22, 35, 5);

            VerificaObjetosIguais(medicamento, medicamentoAtualizado);
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
            //Medicamento medicamento = CriaObjetoMedicamento();
            //Paciente paciente = new Paciente("Joao da silva", "123456789123456");
            //Funcionario funcionario = new Funcionario("Nome funcionario", "admin", "admin");

            //Requisicao requisicao1 = new Requisicao(medicamento, paciente, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario);
            //Requisicao requisicao2 = new Requisicao(medicamento, paciente, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario);
            //Requisicao requisicao3 = new Requisicao(medicamento, paciente, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario);

            //medicamento.AdicionarRequisicao(requisicao1);
            //medicamento.AdicionarRequisicao(requisicao2);
            //medicamento.AdicionarRequisicao(requisicao3);

            Assert.AreEqual(3, 2);
        }

        [TestMethod]
        public void Nao_deve_adicionar_requisicoes_repetidas()
        {
            //Medicamento medicamento = CriaObjetoMedicamento();
            //Paciente paciente = new Paciente("Joao da silva", "123456789123456");
            //Funcionario funcionario = new Funcionario("Nome funcionario", "admin", "admin");

            //Requisicao requisicao1 = new Requisicao(medicamento, paciente, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario);
            //Requisicao requisicao2 = new Requisicao(medicamento, paciente, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario);
            //Requisicao requisicao3 = new Requisicao(medicamento, paciente, 5, new DateTime(2022, 10, 10, 05, 05, 05), funcionario);

            //medicamento.AdicionarRequisicao(requisicao1);
            //medicamento.AdicionarRequisicao(requisicao2);
            //medicamento.AdicionarRequisicao(requisicao3);

            Assert.AreEqual(1, 2);
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
            return new Medicamento("Dipirona", "descrição dipirona", "123", new DateTime(2022, 10, 10, 22, 35, 5));
        }
        private static void VerificaObjetosIguais(Medicamento medicamento, Medicamento medicamentoAtualizado)
        {
            medicamento.AtualizarRegistro(medicamentoAtualizado);

            Assert.AreEqual(true, medicamentoAtualizado.Equals(medicamento));
        }
        #endregion
    }
}
