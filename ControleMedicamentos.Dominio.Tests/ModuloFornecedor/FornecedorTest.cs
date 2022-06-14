using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloFornecedor
{
    [TestClass]
    public class FornecedorTest
    {
        static string Nome = "Fornecedor nome";
        static string Telefone = "49 99999-9999";
        static string Email = "testeteste.com";
        static string Cidade = "Teste";
        static string Estado = "Santa Catarina";
        static Fornecedor fornecedor = new Fornecedor(Nome, Telefone, Email, Cidade, Estado);

        [TestMethod]
        public void Atualizar_nome()
        {
            var fornecedorAlterado = new Fornecedor();

            fornecedorAlterado.Nome = "Teste";

            fornecedor.AtualizarRegistro(fornecedorAlterado);

            Assert.AreEqual("Teste", fornecedor.Nome);
        }
        [TestMethod]
        public void Atualizar_telefone()
        {
            var fornecedorAlterado = new Fornecedor();

            fornecedorAlterado.Telefone = "Teste";

            fornecedor.AtualizarRegistro(fornecedorAlterado);

            Assert.AreEqual("Teste", fornecedor.Telefone);
        }
        [TestMethod]
        public void Atualizar_email()
        {
            var fornecedorAlterado = new Fornecedor();

            fornecedorAlterado.Email = "email_alterado@teste.com";

            fornecedor.AtualizarRegistro(fornecedorAlterado);

            Assert.AreEqual("email_alterado@teste.com", fornecedor.Email);
        }
        [TestMethod]
        public void Atualizar_cidade()
        {
            var fornecedorAlterado = new Fornecedor();

            fornecedorAlterado.Cidade = "cidade_alterada";

            fornecedor.AtualizarRegistro(fornecedorAlterado);

            Assert.AreEqual("cidade_alterada", fornecedor.Cidade);
        }
        [TestMethod]
        public void Atualizar_estado()
        {
            var fornecedorAlterado = new Fornecedor();

            fornecedorAlterado.Estado = "estado_alterado";

            fornecedor.AtualizarRegistro(fornecedorAlterado);

            Assert.AreEqual("estado_alterado", fornecedor.Estado);
        }
        [TestMethod]
        public void Repesentacao_do_objeto()
        {
            var fornecedorAlterado = new Fornecedor();

            fornecedorAlterado.Id = 1;
            fornecedorAlterado.Nome = "Nome";
            fornecedorAlterado.Telefone = "Telefone";

            Assert.AreEqual("Numero: 1 | Nome: Nome | Telefone: Telefone", fornecedorAlterado.ToString());
        }

        [TestMethod]
        public void Construtor_vazio()
        {
            var fornecedorAlterado = new Fornecedor();

            Assert.AreEqual(0, fornecedorAlterado.Id);
            Assert.AreEqual(null, fornecedorAlterado.Nome);
            Assert.AreEqual(null, fornecedorAlterado.Telefone);
            Assert.AreEqual(null, fornecedorAlterado.Email);
            Assert.AreEqual(null, fornecedorAlterado.Cidade);
            Assert.AreEqual(null, fornecedorAlterado.Estado);
        }
        [TestMethod]
        public void Contrutor_todos_parametros_objeto()
        {
            var fornecedorAlterado = new Fornecedor("Nome", "Telefone", "Email", "Cidade", "Estado");

            Assert.AreEqual(0, fornecedorAlterado.Id);
            Assert.AreEqual("Nome", fornecedorAlterado.Nome);
            Assert.AreEqual("Telefone", fornecedorAlterado.Telefone);
            Assert.AreEqual("Email", fornecedorAlterado.Email);
            Assert.AreEqual("Cidade", fornecedorAlterado.Cidade);
            Assert.AreEqual("Estado", fornecedorAlterado.Estado);
        }


    }
}
