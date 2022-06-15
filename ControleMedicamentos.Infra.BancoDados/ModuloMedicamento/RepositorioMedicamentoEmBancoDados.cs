using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados : IRepositorioMedicamento
    {
        private const string enderecoBanco =
             "Data Source=(LocalDB)\\MSSqlLocalDB;" +
             "Initial Catalog=ControleMedicamentoDb;" +
             "Integrated Security=True;" +
             "Pooling=False";
        public string sqlInserir =>
             @"INSERT INTO [TBMEDICAMENTO] 
                (
                    [NOME],
                    [DESCRICAO],
                    [LOTE],
                    [VALIDADE],
                    [QUANTIDADEDISPONIVEL],
                    [FORNECEDOR_ID]
	            )
	            VALUES
                (
                    @NOME,
                    @DESCRICAO,
                    @LOTE,
                    @VALIDADE,
                    @QUANTIDADEDISPONIVEL,
                    @FORNECEDOR_ID
                );
				SELECT SCOPE_IDENTITY()";

        public string sqlEditar =>
            @"UPDATE [TBMEDICAMENTO]	
                SET
	                [NOME] = @NOME,
                    [DESCRICAO] = @DESCRICAO,
                    [LOTE] = @LOTE,
                    [VALIDADE] = @VALIDADE,
                    [QUANTIDADEDISPONIVEL] = @QUANTIDADEDISPONIVEL,
                    [FORNECEDOR_ID] = @FORNECEDOR_ID
                WHERE
	                [ID] = @ID;";

        public string sqlExcluir =>
            @"DELETE FROM[TBMEDICAMENTO]

                WHERE
                    [ID] = @ID";

        public string sqlSelecionarTodos =>
            @"SELECT 
	                MEDICAMENTO.ID,
	                MEDICAMENTO.NOME,
	                MEDICAMENTO.DESCRICAO,
	                MEDICAMENTO.LOTE,
	                MEDICAMENTO.VALIDADE,
	                MEDICAMENTO.QUANTIDADEDISPONIVEL,
	                FORNECEDOR.ID AS FORNECEDOR_ID,
	                FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                FORNECEDOR.EMAIL,
	                FORNECEDOR.TELEFONE,
	                FORNECEDOR.ESTADO,
	                FORNECEDOR.CIDADE

                FROM TBMEDICAMENTO AS MEDICAMENTO

                INNER JOIN TBFORNECEDOR AS FORNECEDOR

                ON FORNECEDOR.ID = MEDICAMENTO.FORNCEDOR_ID";

        public string sqlSelecionarPorId =>
             @"SELECT 
	                MEDICAMENTO.ID,
	                MEDICAMENTO.NOME,
	                MEDICAMENTO.DESCRICAO,
	                MEDICAMENTO.LOTE,
	                MEDICAMENTO.VALIDADE,
	                MEDICAMENTO.QUANTIDADEDISPONIVEL,
	                FORNECEDOR.ID AS FORNECEDOR_ID,
	                FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                FORNECEDOR.EMAIL,
	                FORNECEDOR.TELEFONE,
	                FORNECEDOR.ESTADO,
	                FORNECEDOR.CIDADE

                FROM TBMEDICAMENTO AS MEDICAMENTO

                INNER JOIN TBFORNECEDOR AS FORNECEDOR

                ON FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID

                WHERE MEDICAMENTO.ID = @ID";

        public void ConfigurarParametrosMedicamento(Medicamento novomedicamento, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novomedicamento.Id);
            comando.Parameters.AddWithValue("NOME", novomedicamento.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", novomedicamento.Descricao);
            comando.Parameters.AddWithValue("LOTE", novomedicamento.Lote);
            comando.Parameters.AddWithValue("VALIDADE", novomedicamento.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", novomedicamento.QuantidadeDisponivel);
            comando.Parameters.AddWithValue("FORNECEDOR_ID", novomedicamento.Fornecedor.Id);
        }

        public Medicamento ConverterParaMedicamento(SqlDataReader leitormedicamento)
        {
            int id = Convert.ToInt32(leitormedicamento["ID"]);
            string nome = Convert.ToString(leitormedicamento["NOME"]);
            string login = Convert.ToString(leitormedicamento["DESCRICAO"]);
            string lote = Convert.ToString(leitormedicamento["LOTE"]);
            DateTime validade = Convert.ToDateTime(leitormedicamento["VALIDADE"]);
            int quantidadeDisponivel = Convert.ToInt32(leitormedicamento["QUANTIDADEDISPONIVEL"]);
            int fornecedor_id = Convert.ToInt32(leitormedicamento["FORNECEDOR_ID"]);
            string fornecedor_nome = Convert.ToString(leitormedicamento["FORNECEDOR_NOME"]);
            string fornecedor_telefone = Convert.ToString(leitormedicamento["TELEFONE"]);
            string fornecedor_email = Convert.ToString(leitormedicamento["EMAIL"]);
            string fornecedor_cidade = Convert.ToString(leitormedicamento["CIDADE"]);
            string fornecedor_estado = Convert.ToString(leitormedicamento["ESTADO"]);

            var fornecedor = new Fornecedor
            {
                Id = fornecedor_id,
                Nome = fornecedor_nome,
                Telefone = fornecedor_telefone,
                Email = fornecedor_email,
                Cidade = fornecedor_cidade,
                Estado = fornecedor_estado
            };

            var funcionario = new Medicamento(nome, login, lote, validade, quantidadeDisponivel);
            
            funcionario.Id = id;
            funcionario.Fornecedor = fornecedor;
            
            

            return funcionario;
        }

        public ValidationResult Editar(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMedicamento(medicamento, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Medicamento medicamento)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", medicamento.Id);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Inserir(Medicamento medicamento)
        {
            var validador = new ValidadorMedicamento();

            var resultadoValidacao = validador.Validate(medicamento);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMedicamento(medicamento, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            medicamento.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Medicamento SelecionarPorId(Medicamento medicamento)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", medicamento.Id);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            Medicamento medicamentoSelecionado = null;

            if (leitorMedicamento.Read())
                medicamentoSelecionado = ConverterParaMedicamento(leitorMedicamento);

            conexaoComBanco.Close();

            return medicamentoSelecionado;
        }

        public List<Medicamento> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMedicamento = comandoSelecao.ExecuteReader();

            List<Medicamento> funcionarios = new List<Medicamento>();

            while (leitorMedicamento.Read())
            {
                Medicamento funcionario = ConverterParaMedicamento(leitorMedicamento);

                funcionarios.Add(funcionario);
            }

            conexaoComBanco.Close();

            return funcionarios;
        }
    }
}
