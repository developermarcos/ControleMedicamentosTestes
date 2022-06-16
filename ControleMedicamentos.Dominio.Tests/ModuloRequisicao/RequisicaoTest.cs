using System;
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloRequisicao
{
    [TestClass]
    public class RequisicaoTest
    {
        [TestMethod]
        public void Atualizar_quantidade()
        {
            int quantidadeMedicamento = 2;
            Requisicao requisicao = ObterRequisicao();
            
            Requisicao requisicaoAtualizada = ObterRequisicao();
            requisicaoAtualizada.QtdMedicamento = quantidadeMedicamento;

            requisicao.AtualizarRequisicao(requisicaoAtualizada);
            
            
            Assert.AreEqual(quantidadeMedicamento, requisicao.QtdMedicamento);
        }

        [TestMethod]
        public void Atualizar_data()
        {
            DateTime data = new DateTime(2022, 11, 11);

            Requisicao requisicao = ObterRequisicao();

            Requisicao requisicaoAtualizada = ObterRequisicao();
            requisicaoAtualizada.Data = data;

            requisicao.AtualizarRequisicao(requisicaoAtualizada);


            Assert.AreEqual(data, requisicao.Data);
        }

        [TestMethod]
        public void Atualizar_paciente()
        {
            Paciente pacienteAtualizado = new Paciente("Novo paciente", "987654321654321");

            Requisicao requisicao = ObterRequisicao();

            Requisicao requisicaoAtualizada = ObterRequisicao();

            requisicaoAtualizada.Paciente.AtualizarRegistro(pacienteAtualizado);

            requisicao.AtualizarRequisicao(requisicaoAtualizada);


            Assert.AreEqual(true, pacienteAtualizado.Equals(requisicao.Paciente));
        }

        [TestMethod]
        public void Atualizar_Funcionario()
        {
            Funcionario funcionarioAtualizado = new Funcionario("Nome funcionario atualizado", "login1", "senha1");

            Requisicao requisicao = ObterRequisicao();

            Requisicao requisicaoAtualizada = ObterRequisicao();

            requisicaoAtualizada.Funcionario.AtualizarRegistro(funcionarioAtualizado);

            requisicao.AtualizarRequisicao(requisicaoAtualizada);


            Assert.AreEqual(true, funcionarioAtualizado.Equals(requisicao.Funcionario));
        }

        [TestMethod]
        public void Atualizar_Medicamento()
        {
            Medicamento medicamentoAtualizado = new Medicamento("Atualizado", "descricao atualizada", "321", new DateTime(2022, 11, 11), 8);

            Requisicao requisicao = ObterRequisicao();

            Requisicao requisicaoAtualizada = ObterRequisicao();

            requisicaoAtualizada.Medicamento.AtualizarRegistro(medicamentoAtualizado);

            requisicao.AtualizarRequisicao(requisicaoAtualizada);


            Assert.AreEqual(true, medicamentoAtualizado.Equals(requisicao.Medicamento));
        }

        [TestMethod]
        public void Quantidade_medicamento_requisicao_deve_ser_menor_quantidade_medicamento_disponivel()
        {
            Requisicao requisicao = ObterRequisicao();

            requisicao.QtdMedicamento = 5;

            Assert.AreEqual(4, requisicao.QtdMedicamento);
        }

        private Requisicao ObterRequisicao()
        {
            Paciente paciente = new Paciente("Paciente 1", "123456789123456");
            Fornecedor fornecedor = new Fornecedor("Nome fornecedor", "55 49 9999-0000", "teste@teste.com", "cidade", "estado");
            Funcionario funcionario = new Funcionario("Nome funcionario", "admin1", "admin1");
            Medicamento medicamento = new Medicamento("Nome medicamento", "descricao", "123" , new DateTime(2022,10,10), 4);
            Requisicao requisicao = new Requisicao(medicamento, paciente, 3, new DateTime(2022,10,10), funcionario);

            return requisicao;
        }
    }
}
