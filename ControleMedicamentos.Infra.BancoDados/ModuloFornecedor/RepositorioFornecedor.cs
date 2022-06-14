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

        public string sqlEditar => "";

        public string sqlExcluir => "";

        public string sqlSelecionarTodos => "";

        public string sqlSelecionarPorId => "";

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
            throw new NotImplementedException();
        }

        public ValidationResult Excluir(Fornecedor fornecedor)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public List<Fornecedor> SelecionarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
