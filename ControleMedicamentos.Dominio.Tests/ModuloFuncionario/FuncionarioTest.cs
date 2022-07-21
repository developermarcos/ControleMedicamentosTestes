using System;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloFuncionario
{
    [TestClass]
    public class FuncionarioTest
    {
        
        [TestMethod]
        public void Atualizar_nome()
        {
            Funcionario funcionario = new Funcionario();

            funcionario.Nome = "Nome";
            
            Funcionario funcionarioAlterado = new Funcionario();

            funcionarioAlterado.Nome = "Nome_alterado";

            funcionario.AtualizarRegistro(funcionarioAlterado);

            Assert.AreEqual("Nome_alterado", funcionario.Nome);
        }

        [TestMethod]
        public void Atualizar_login()
        {
            Funcionario funcionario = new Funcionario();

            funcionario.Login = "Login";

            Funcionario funcionarioAlterado = new Funcionario();

            funcionarioAlterado.Login = "Login_alterado";

            funcionario.AtualizarRegistro(funcionarioAlterado);

            Assert.AreEqual("Login_alterado", funcionario.Login);
        }

        [TestMethod]
        public void Atualizar_senha()
        {
            Funcionario funcionario = new Funcionario();

            funcionario.Senha = "Senha";

            Funcionario funcionarioAlterado = new Funcionario();

            funcionarioAlterado.Senha = "Senha_alterado";

            funcionario.AtualizarRegistro(funcionarioAlterado);

            Assert.AreEqual("Senha_alterado", funcionario.Senha);
        }

        [TestMethod]
        public void Contrutor_vazio()
        {
            Funcionario funcionario = new Funcionario();

            Assert.AreEqual(null, funcionario.Nome);
            Assert.AreEqual(null, funcionario.Login);
            Assert.AreEqual(null, funcionario.Senha);
        }
        [TestMethod]
        public void Contrutor_todos_parametros()
        {
            Funcionario funcionario = new Funcionario("Nome", "Login", "Senha");

            Assert.AreEqual("Nome", funcionario.Nome);
            Assert.AreEqual("Login", funcionario.Login);
            Assert.AreEqual("Senha", funcionario.Senha);
        }
    }
}
