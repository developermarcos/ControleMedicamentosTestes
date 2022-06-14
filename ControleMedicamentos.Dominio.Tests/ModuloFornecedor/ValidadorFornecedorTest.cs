using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentValidation.Results;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleFornecedors.Dominio.ModuloFornecedor;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class ValidadorFornecedorTest
    {
        string Nome = "Fornecedor nome";
        string Telefone = "49 99999-9999";
        string Email = "testeteste.com";
        string Cidade = "Teste";
        string Estado = "Santa Catarina";

        [TestMethod]
        public void Nome_fornecedor_nao_deve_ser_nulo()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = null;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Nome' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Nome_fornecedor_nao_pode_ser_vazio()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = "";

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo Nome não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_fornecedor_deve_seguir_formato_email()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = Telefone;
            fornecedor.Email = "teste.com";
            fornecedor.Cidade = Cidade;
            fornecedor.Estado = Estado;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Formato de Email invalido.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email__fornecedor_nao_deve_ser_nulo()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = Telefone;
            fornecedor.Email = null;
            fornecedor.Cidade = Cidade;
            fornecedor.Estado = Estado;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Email' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Email_fornecedor_nao_deve_ser_vazio()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = Telefone;
            fornecedor.Email = "";
            fornecedor.Cidade = Cidade;
            fornecedor.Estado = Estado;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Email' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Telefone_fornecedor_nao_deve_ser_nulo()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = null;
            fornecedor.Email = Email;
            fornecedor.Cidade = Cidade;
            fornecedor.Estado = Estado;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Telefone' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }
        
        [TestMethod]
        public void Telefone_fornecedor_nao_deve_ser_vazio()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = "";
            fornecedor.Email = Email;
            fornecedor.Cidade = Cidade;
            fornecedor.Estado = Estado;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Telefone' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_fornecedor_nao_deve_ser_nulo()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = Telefone;
            fornecedor.Email = Email;
            fornecedor.Cidade = null;
            fornecedor.Estado = Estado;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Cidade' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Cidade_fornecedor_nao_deve_ser_vazio()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = Telefone;
            fornecedor.Email = Email;
            fornecedor.Cidade = "";
            fornecedor.Estado = Estado;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Cidade' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_fornecedor_nao_deve_ser_nulo()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = Telefone;
            fornecedor.Email = Email;
            fornecedor.Cidade = Cidade;
            fornecedor.Estado = null;

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Estado' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Estado_fornecedor_nao_deve_ser_vazio()
        {
            var fornecedor = new Fornecedor();

            fornecedor.Nome = Nome;
            fornecedor.Telefone = Telefone;
            fornecedor.Email = Email;
            fornecedor.Cidade = Cidade;
            fornecedor.Estado = "";

            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            Assert.AreEqual("Campo 'Estado' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }
    }
}
