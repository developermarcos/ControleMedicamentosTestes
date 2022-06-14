using ControleMedicamentos.Dominio.ModuloFuncionario;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class RepositorioFuncionario :  IRepositorioFuncionario
    {
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=ControleMedicamentoDb;" +
              "Integrated Security=True;" +
              "Pooling=False";
        public string sqlInserir =>
            @"INSERT INTO TBFUNCIONARIO 
                (
                    [NOME],
                    [LOGIN],
                    [SENHA]
	            )
	            VALUES
                (
                    @NOME,
                    @LOGIN,
                    @SENHA
                );
				SELECT SCOPE_IDENTITY()";

        public string sqlEditar =>
            @"UPDATE TBFUNCIONARIO	
                SET
	                [NOME] = @NOME,
	                [LOGIN] = @LOGIN,
	                [SENHA] = @SENHA
                WHERE
	                [ID] = @ID;";

        public string sqlExcluir =>
            @"DELETE FROM[TBFUNCIONARIO]

                WHERE
                    [ID] = @ID";

        public string sqlSelecionarTodos =>
            @"SELECT 
	                FUNCIONARIO.ID,
	                FUNCIONARIO.NOME,
	                FUNCIONARIO.LOGIN,
	                FUNCIONARIO.SENHA

                FROM TBFUNCIONARIO AS FUNCIONARIO;";

        public string sqlSelecionarPorId =>
            @"SELECT 
	                FUNCIONARIO.ID,
	                FUNCIONARIO.NOME,
	                FUNCIONARIO.LOGIN,
	                FUNCIONARIO.SENHA

                FROM TBFUNCIONARIO AS FUNCIONARIO

                WHERE ID = @ID";

        public void ConfigurarParametrosFuncionario(Funcionario novoFuncionario, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoFuncionario.Id);
            comando.Parameters.AddWithValue("NOME", novoFuncionario.Nome);
            comando.Parameters.AddWithValue("LOGIN", novoFuncionario.Login);
            comando.Parameters.AddWithValue("SENHA", novoFuncionario.Senha);
        }

        public Funcionario ConverterParaFuncionario(SqlDataReader leitorFuncionario)
        {
            int id = Convert.ToInt32(leitorFuncionario["ID"]);
            string nome = Convert.ToString(leitorFuncionario["NOME"]);
            string login = Convert.ToString(leitorFuncionario["LOGIN"]);
            string senha = Convert.ToString(leitorFuncionario["SENHA"]);

            var funcionario = new Funcionario
            {
                Id = id,
                Nome = nome,
                Login = login,
                Senha = senha
            };

            return funcionario;
        }

        public ValidationResult Editar(Funcionario funcionario)
        {
            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFuncionario(funcionario, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Funcionario funcionario)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", funcionario.Id);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Inserir(Funcionario funcionario)
        {
            var validador = new ValidadorFuncionario();

            var resultadoValidacao = validador.Validate(funcionario);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFuncionario(funcionario, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            funcionario.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Funcionario SelecionarPorId(Funcionario funcionarioPesquisa)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", funcionarioPesquisa.Id);

            conexaoComBanco.Open();
            SqlDataReader leitorFuncionario = comandoSelecao.ExecuteReader();

            Funcionario funcionarioSelecionado = null;

            if (leitorFuncionario.Read())
                funcionarioSelecionado = ConverterParaFuncionario(leitorFuncionario);

            conexaoComBanco.Close();

            return funcionarioSelecionado;
        }

        public List<Funcionario> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorFuncionario = comandoSelecao.ExecuteReader();

            List<Funcionario> funcionarios = new List<Funcionario>();

            while (leitorFuncionario.Read())
            {
                Funcionario funcionario = ConverterParaFuncionario(leitorFuncionario);

                funcionarios.Add(funcionario);
            }

            conexaoComBanco.Close();

            return funcionarios;
        }
    }
}
