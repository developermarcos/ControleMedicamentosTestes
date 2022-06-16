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
    public class ValidadorRequisicaoTest
    {
        [TestMethod]
        public void Data_requisicao_nao_deve_ser_vazio()
        {
            Requisicao requisicao = ObterRequisicao();

            var validador = new ValidadorRequisicao();

            requisicao.Data = default;

            var resultadoValidacao = validador.Validate(requisicao);

            Assert.AreEqual("Campo 'Data' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Data_requisicao_nao_deve_ser_menor_que_data_atual()
        {
            Requisicao requisicao = ObterRequisicao();

            var validador = new ValidadorRequisicao();

            requisicao.Data = new DateTime(2022,1,1);

            var resultadoValidacao = validador.Validate(requisicao);

            Assert.AreEqual("Data não pode ser menor que data atual.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Quantidade_medicamento_requisicao_nao_deve_ser_vazio()
        {
            Requisicao requisicao = ObterRequisicao();

            var validador = new ValidadorRequisicao();

            requisicao.QtdMedicamento = default;

            var resultadoValidacao = validador.Validate(requisicao);

            Assert.AreEqual("Campo 'QtdMedicamento' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Quantidade_medicamento_requisicao_nao_deve_ser_zero()
        {
            Requisicao requisicao = ObterRequisicao();

            var validador = new ValidadorRequisicao();

            requisicao.QtdMedicamento = -1;

            var resultadoValidacao = validador.Validate(requisicao);

            Assert.AreEqual("Quantidade de medicamento deve ser maior que zero.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        #region Métodos privados
        private Requisicao ObterRequisicao()
        {
            Paciente paciente = new Paciente("Paciente 1", "123456789123456");
            Fornecedor fornecedor = new Fornecedor("Nome fornecedor", "55 49 9999-0000", "teste@teste.com", "cidade", "estado");
            Funcionario funcionario = new Funcionario("Nome funcionario", "admin1", "admin1");
            Medicamento medicamento = new Medicamento("Nome medicamento", "descricao", "123", new DateTime(2022, 10, 10), 4);
            Requisicao requisicao = new Requisicao(medicamento, paciente, 3, new DateTime(2022, 10, 10), funcionario);

            return requisicao;
        }
        #endregion
    }
}
