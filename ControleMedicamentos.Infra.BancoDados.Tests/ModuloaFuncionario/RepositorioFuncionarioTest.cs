using System.Collections.Generic;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Infra.BancoDados.Tests.ModuloaFuncionario
{
    [TestClass]
    public class RepositorioFuncionarioTest : BaseTest
    {
        [TestMethod]
        public void Deve_inserir_funcionario()
        {
            var validationResult = repositorioFuncionario.Inserir(ObterFuncionario());

            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_Editar_funcionario()
        {
            Funcionario funcionario = ObterFuncionario();

            repositorioFuncionario.Inserir(funcionario);

            funcionario.Nome= "Marcos Lima Editar";

            var validationResult = repositorioFuncionario.Editar(funcionario);
            
            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_excluir_funcionario()
        {
            Funcionario funcionario = ObterFuncionario();

            repositorioFuncionario.Inserir(funcionario);

            var validationResult = repositorioFuncionario.Excluir(funcionario);
            
            Assert.AreEqual(true, validationResult.IsValid);
        }

        [TestMethod]
        public void Deve_selecionar_todos_registros_de_funcionario()
        {
            Funcionario funcionario = ObterFuncionario();

            Funcionario funcionario2 = ObterFuncionario();
            funcionario2.Nome = "Funcionario 2";

            Funcionario funcionario3 = ObterFuncionario();
            funcionario3.Nome = "Funcionario 3";

            List<Funcionario> funcionariosInserir = new List<Funcionario> 
            {
                funcionario,
                funcionario2,
                funcionario3
            };
            
            foreach (var item in funcionariosInserir)
                repositorioFuncionario.Inserir(item);

            var todosFuncionarios = repositorioFuncionario.SelecionarTodos();

            Assert.AreEqual(3, todosFuncionarios.Count);
        }

        [TestMethod]
        public void Deve_selecionar_registro_de_funcionario_por_id()
        {
            Funcionario funcionarioInserir = ObterFuncionario();

            repositorioFuncionario.Inserir(funcionarioInserir);

            Funcionario funcionarioPesquisa = repositorioFuncionario.SelecionarPorId(funcionarioInserir);

            Assert.AreEqual(true, funcionarioPesquisa.Equals(funcionarioInserir));
        }

        [TestMethod]
        public void Nao_deve_inserir_funcionario_nome_repetido()
        {
            //Funcionario funcionario = new Funcionario("Marcos", "admin1", "Admin1");

            //var validationResult = repositorioFuncionario.Inserir(funcionario);

            //Assert.AreEqual(true, validationResult.IsValid);
        }
    }
}
