using System;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class ValidadorFuncionarioTest
    {
        [TestMethod]
        public void Nome_funcionario_nao_deve_ser_nulo()
        {
            Funcionario funcionario = new Funcionario();

            funcionario.Nome = null;
            funcionario.Login = "Login1";
            funcionario.Senha = "Senha1";

            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Nome' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
            //Assert.AreEqual("Campo 'Nome' não pode ser nulo.", "");
        }

        [TestMethod]
        public void Nome_funcionario_nao_pode_ser_vazio()
        {
            var funcionario = new Funcionario();

            funcionario.Nome = "";
            funcionario.Login = "Login1";
            funcionario.Senha = "Senha1";

            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Nome' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }
        [TestMethod]
        public void Nome_funcionario_deve_conter_no_minimo_6_digitos()
        {
            var funcionario = new Funcionario();

            funcionario.Nome = "Nome1";
            funcionario.Login = "Login1";
            funcionario.Senha = "Senha1";

            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Nome' deve conter pelo menos 6 digitos.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Login_funcionario_nao_deve_ser_nulo()
        {
            var funcionario = new Funcionario();

            funcionario.Nome = "Nome teste";
            funcionario.Login = null;
            funcionario.Senha = "Senha1";
            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Login' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Login_funcionario_nao_pode_ser_vazio()
        {
            var funcionario = new Funcionario();

            funcionario.Nome = "Nome teste";
            funcionario.Login = "";
            funcionario.Senha = "Senha1";

            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Login' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Login_funcionario_deve_conter_no_minio_4_digitos()
        {
            var funcionario = new Funcionario();

            funcionario.Nome = "Nome teste";
            funcionario.Login = "123";
            funcionario.Senha = "Senha1";

            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Login' deve conter pelo menos 4 digitos.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_funcionario_nao_deve_ser_nulo()
        {
            var funcionario = new Funcionario();

            funcionario.Nome = "Nome teste";
            funcionario.Login = "Login";
            funcionario.Senha = null;
            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Senha' não pode ser nulo.", resultadoValidacao.Errors[0].ErrorMessage);
        }

        [TestMethod]
        public void Senha_funcionario_nao_pode_ser_vazio()
        {
            var funcionario = new Funcionario();

            funcionario.Nome = "Nome teste";
            funcionario.Login = "Login";
            funcionario.Senha = "";

            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Senha' não pode ser vazio.", resultadoValidacao.Errors[0].ErrorMessage);
        }
        [TestMethod]
        public void Senha_funcionario_deve_conter_no_minio_6_digitos()
        {
            var funcionario = new Funcionario();

            funcionario.Nome = "Nome teste";
            funcionario.Login = "Login";
            funcionario.Senha = "12345";

            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            Assert.AreEqual("Campo 'Senha' deve conter pelo menos 6 digitos.", resultadoValidacao.Errors[0].ErrorMessage);
        }

    }
}
