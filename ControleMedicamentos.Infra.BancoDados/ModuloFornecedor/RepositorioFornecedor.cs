using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedor : IRepositorioFornecedor
    {
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=ControleMedicamentoDb;" +
              "Integrated Security=True;" +
              "Pooling=False";
        public string sqlInserir =>
            @"INSERT INTO TBFORNECEDOR
                (
                    [NOME],
                    [EMAIL],
                    [TELEFONE],
                    [CIDADE],
                    [ESTADO]
	            )
	            VALUES
                (
                    @NOME,
                    @EMAIL,
                    @TELEFONE,
                    @CIDADE,
                    @ESTADO
                );
				SELECT SCOPE_IDENTITY()";

        public string sqlEditar =>
            @"UPDATE TBFORNECEDOR	
                SET
	                [NOME] = @NOME,
                    [EMAIL] = @EMAIL,
                    [TELEFONE] = @TELEFONE,
                    [CIDADE] = @CIDADE,
                    [ESTADO] = @ESTADO
                WHERE
	                [ID] = @ID;";

        public string sqlExcluir =>
            @"DELETE FROM[TBFORNECEDOR]

                WHERE
                    [ID] = @ID";

        public string sqlSelecionarTodos =>
            @"SELECT
	                FORNECEDOR.ID,
	                FORNECEDOR.NOME,
	                FORNECEDOR.TELEFONE,
	                FORNECEDOR.EMAIL,
	                FORNECEDOR.CIDADE,
	                FORNECEDOR.ESTADO
                
                FROM TBFORNECEDOR AS FORNECEDOR";

        public string sqlSelecionarPorId =>
            @"SELECT
	                FORNECEDOR.ID,
	                FORNECEDOR.NOME,
	                FORNECEDOR.TELEFONE,
	                FORNECEDOR.EMAIL,
	                FORNECEDOR.CIDADE,
	                FORNECEDOR.ESTADO
                
                FROM TBFORNECEDOR AS FORNECEDOR

                WHERE ID = @ID";

        public void ConfigurarParametrosFuncionario(Fornecedor novoFornecedor, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoFornecedor.Id);
            comando.Parameters.AddWithValue("NOME", novoFornecedor.Nome);
            comando.Parameters.AddWithValue("EMAIL", novoFornecedor.Email);
            comando.Parameters.AddWithValue("TELEFONE", novoFornecedor.Telefone);
            comando.Parameters.AddWithValue("CIDADE", novoFornecedor.Cidade);
            comando.Parameters.AddWithValue("ESTADO", novoFornecedor.Estado);
        }

        public Fornecedor ConverterParaFuncionario(SqlDataReader leitorFornecedor)
        {
            int id = Convert.ToInt32(leitorFornecedor["ID"]);
            string nome = Convert.ToString(leitorFornecedor["NOME"]);
            string email = Convert.ToString(leitorFornecedor["EMAIL"]);
            string telefone = Convert.ToString(leitorFornecedor["TELEFONE"]);
            string cidade = Convert.ToString(leitorFornecedor["CIDADE"]);
            string estado = Convert.ToString(leitorFornecedor["ESTADO"]);

            var funcionario = new Fornecedor
            {
                Id = id,
                Nome = nome,
                Email = email,
                Telefone = telefone,
                Cidade = cidade,
                Estado = estado
            };

            return funcionario;
        }

        public ValidationResult Editar(Fornecedor fornecedor)
        {
            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosFuncionario(fornecedor, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Fornecedor fornecedor)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", fornecedor.Id);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Inserir(Fornecedor fornecedor)
        {
            var validador = new ValidadorFornecedor();

            var resultadoValidacao = validador.Validate(fornecedor);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosFuncionario(fornecedor, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();

            fornecedor.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Fornecedor SelecionarPorId(Fornecedor fornecedor)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", fornecedor.Id);

            conexaoComBanco.Open();
            SqlDataReader leitorFuncionario = comandoSelecao.ExecuteReader();

            Fornecedor fornecedorSelecionado = null;

            if (leitorFuncionario.Read())
                fornecedorSelecionado = ConverterParaFuncionario(leitorFuncionario);

            conexaoComBanco.Close();

            return fornecedorSelecionado;
        }

        public List<Fornecedor> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorFornecedor = comandoSelecao.ExecuteReader();

            List<Fornecedor> Fornecedors = new List<Fornecedor>();

            while (leitorFornecedor.Read())
            {
                Fornecedor Fornecedor = ConverterParaFuncionario(leitorFornecedor);

                Fornecedors.Add(Fornecedor);
            }

            conexaoComBanco.Close();

            return Fornecedors;
        }
    }
}
