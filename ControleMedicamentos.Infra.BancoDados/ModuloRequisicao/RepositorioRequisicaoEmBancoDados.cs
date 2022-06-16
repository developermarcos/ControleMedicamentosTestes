
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleRequisicaos.Dominio.ModuloRequisicao;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados : IRepositorioRequisicao
    {
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=ControleMedicamentoDb;" +
              "Integrated Security=True;" +
              "Pooling=False";
        public string sqlInserir =>
             @"INSERT INTO TBREQUISICAO 
                (
                    [DATA],
                    [QUANTIDADEMEDICAMENTO],
                    [FUNCIONARIO_ID],
                    [PACIENTE_ID],
                    [MEDICAMENTO_ID]
	            )
	            VALUES
                (
                    @DATA,
                    @QUANTIDADEMEDICAMENTO,
                    @FUNCIONARIO_ID,
                    @PACIENTE_ID,
                    @MEDICAMENTO_ID
                );
				SELECT SCOPE_IDENTITY()";

        public string sqlEditar =>
            @"UPDATE [TBREQUISICAO]	
                SET
	                DATA = @DATA,
                    QUANTIDADEMEDICAMENTO = @QUANTIDADEMEDICAMENTO,
                    FUNCIONARIO_ID = @FUNCIONARIO_ID,
                    PACIENTE_ID = @PACIENTE_ID,
                    MEDICAMENTO_ID = @MEDICAMENTO_ID
                WHERE
	                [ID] = @ID;";

        public string sqlExcluir =>
            @"DELETE FROM[TBREQUISICAO]

                WHERE
                    [ID] = @ID";

        public string sqlSelecionarTodos =>
            @"
                SELECT 
	                    REQUISICAO.ID AS ID,
	                    REQUISICAO.QUANTIDADEMEDICAMENTO AS QUANTIDADEMEDICAMENTO,
	                    REQUISICAO.DATA AS DATA,

	                    FUNCIONARIO.ID AS FUNCIONARIO_ID,
	                    FUNCIONARIO.NOME AS FUNCIONARIO_NOME,
	                    FUNCIONARIO.LOGIN AS FUNCIONARIO_LOGIN,
	                    FUNCIONARIO.SENHA AS FUNCIONARIO_SENHA,

	                    PACIENTE.ID AS PACIENTE_ID,
	                    PACIENTE.NOME AS PACIENTE_NOME,
	                    PACIENTE.CARTAOSUS AS PACIENTE_CARTAOSUS,

	                    MEDICAMENTO.ID AS MEDICAMENTO_ID,
	                    MEDICAMENTO.NOME AS MEDICAMENTO_NOME,
	                    MEDICAMENTO.DESCRICAO AS MEDICAMENTO_DESCRICAO,
	                    MEDICAMENTO.LOTE AS MEDICAMENTO_LOTE,
	                    MEDICAMENTO.VALIDADE AS MEDICAMENTO_VALIDADE,
	                    MEDICAMENTO.QUANTIDADEDISPONIVEL AS MEDICAMENTO_QTDDISPONIVEL,

	                    FORNECEDOR.ID AS FORNECEDOR_ID,
	                    FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                    FORNECEDOR.EMAIL AS FORNECEDOR_EMAIL,
	                    FORNECEDOR.TELEFONE AS FORNECEDOR_TELEFONE,
	                    FORNECEDOR.CIDADE AS FORNECEDOR_CIDADE,
	                    FORNECEDOR.ESTADO AS FORNECEDOR_ESTADO


                    FROM [TBREQUISICAO] AS REQUISICAO

                    INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                    ON MEDICAMENTO.ID = REQUISICAO.MEDICAMENTO_ID

                    INNER JOIN [TBFUNCIONARIO] AS FUNCIONARIO
                    ON FUNCIONARIO.ID = REQUISICAO.FUNCIONARIO_ID

                    INNER JOIN [TBPACIENTE] AS PACIENTE
                    ON PACIENTE.ID = REQUISICAO.PACIENTE_ID

                    INNER JOIN [TBFORNECEDOR] AS FORNECEDOR
                    ON FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID";

        public string sqlSelecionarPorId =>
            @"
                SELECT 
	                    REQUISICAO.ID AS ID,
	                    REQUISICAO.QUANTIDADEMEDICAMENTO AS QUANTIDADEMEDICAMENTO,
	                    REQUISICAO.DATA AS DATA,

	                    FUNCIONARIO.ID AS FUNCIONARIO_ID,
	                    FUNCIONARIO.NOME AS FUNCIONARIO_NOME,
	                    FUNCIONARIO.LOGIN AS FUNCIONARIO_LOGIN,
	                    FUNCIONARIO.SENHA AS FUNCIONARIO_SENHA,

	                    PACIENTE.ID AS PACIENTE_ID,
	                    PACIENTE.NOME AS PACIENTE_NOME,
	                    PACIENTE.CARTAOSUS AS PACIENTE_CARTAOSUS,

	                    MEDICAMENTO.ID AS MEDICAMENTO_ID,
	                    MEDICAMENTO.NOME AS MEDICAMENTO_NOME,
	                    MEDICAMENTO.DESCRICAO AS MEDICAMENTO_DESCRICAO,
	                    MEDICAMENTO.LOTE AS MEDICAMENTO_LOTE,
	                    MEDICAMENTO.VALIDADE AS MEDICAMENTO_VALIDADE,
	                    MEDICAMENTO.QUANTIDADEDISPONIVEL AS MEDICAMENTO_QTDDISPONIVEL,

	                    FORNECEDOR.ID AS FORNECEDOR_ID,
	                    FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                    FORNECEDOR.EMAIL AS FORNECEDOR_EMAIL,
	                    FORNECEDOR.TELEFONE AS FORNECEDOR_TELEFONE,
	                    FORNECEDOR.CIDADE AS FORNECEDOR_CIDADE,
	                    FORNECEDOR.ESTADO AS FORNECEDOR_ESTADO


                    FROM [TBREQUISICAO] AS REQUISICAO

                    INNER JOIN [TBMEDICAMENTO] AS MEDICAMENTO
                    ON MEDICAMENTO.ID = REQUISICAO.MEDICAMENTO_ID

                    INNER JOIN [TBFUNCIONARIO] AS FUNCIONARIO
                    ON FUNCIONARIO.ID = REQUISICAO.FUNCIONARIO_ID

                    INNER JOIN [TBPACIENTE] AS PACIENTE
                    ON PACIENTE.ID = REQUISICAO.PACIENTE_ID

                    INNER JOIN [TBFORNECEDOR] AS FORNECEDOR
                    ON FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID

                    WHERE REQUISICAO.ID = @ID";

        public void ConfigurarParametrosRequisicao(Requisicao novoRequisicao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoRequisicao.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", novoRequisicao.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", novoRequisicao.Data);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", novoRequisicao.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", novoRequisicao.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", novoRequisicao.Medicamento.Id);
        }

        public Requisicao ConverterParaRequisicao(SqlDataReader leitorRequisicao)
        {
            int id = Convert.ToInt32(leitorRequisicao["ID"]);
            int QtdMedicamento = Convert.ToInt32(leitorRequisicao["QUANTIDADEMEDICAMENTO"]);
            DateTime data = Convert.ToDateTime(leitorRequisicao["DATA"]);

            int funcionario_id = Convert.ToInt32(leitorRequisicao["FUNCIONARIO_ID"]);
            string funcionario_nome = Convert.ToString(leitorRequisicao["FUNCIONARIO_NOME"]);
            string funcionario_login = Convert.ToString(leitorRequisicao["FUNCIONARIO_LOGIN"]);
            string funcionario_senha = Convert.ToString(leitorRequisicao["FUNCIONARIO_SENHA"]);

            int paciente_id = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            string paciente_nome = Convert.ToString(leitorRequisicao["PACIENTE_NOME"]);
            string paciente_cartaosus = Convert.ToString(leitorRequisicao["PACIENTE_CARTAOSUS"]);

            int medicamento_id = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_ID"]);
            string medicamento_nome = Convert.ToString(leitorRequisicao["MEDICAMENTO_NOME"]);
            string medicamento_descricao = Convert.ToString(leitorRequisicao["MEDICAMENTO_DESCRICAO"]);
            string medicamento_lote = Convert.ToString(leitorRequisicao["MEDICAMENTO_LOTE"]);
            DateTime medicamento_validade = Convert.ToDateTime(leitorRequisicao["MEDICAMENTO_VALIDADE"]);
            int medicamento_qtddisponivel = Convert.ToInt32(leitorRequisicao["MEDICAMENTO_QTDDISPONIVEL"]);

            int Fornecedor_id = Convert.ToInt32(leitorRequisicao["FORNECEDOR_ID"]);
            string Fornecedor_nome = Convert.ToString(leitorRequisicao["FORNECEDOR_NOME"]);
            string Fornecedor_email = Convert.ToString(leitorRequisicao["FORNECEDOR_EMAIL"]);
            string Fornecedor_telefone = Convert.ToString(leitorRequisicao["FORNECEDOR_TELEFONE"]);
            string Fornecedor_cidade = Convert.ToString(leitorRequisicao["FORNECEDOR_CIDADE"]);
            string Fornecedor_estado = Convert.ToString(leitorRequisicao["FORNECEDOR_ESTADO"]);

            var fornecedor = new Fornecedor
            {
                Id = Fornecedor_id,
                Nome = Fornecedor_nome,
                Email = Fornecedor_email,
                Telefone = Fornecedor_telefone,
                Cidade = Fornecedor_cidade,
                Estado = Fornecedor_estado
            };

            var paciente = new Paciente
            {
                Id = paciente_id,
                Nome = paciente_nome,
                CartaoSUS = paciente_cartaosus
            };

            var funcionario = new Funcionario
            {
                Id = funcionario_id,
                Nome = funcionario_nome,
                Login = funcionario_login,
                Senha = funcionario_senha
            };

            var medicamento = new Medicamento
            {
                Id = medicamento_id,
                Nome = medicamento_nome,
                Descricao = medicamento_descricao,
                Lote = medicamento_lote,
                Validade = medicamento_validade,
                QuantidadeDisponivel = medicamento_qtddisponivel,
                Fornecedor = fornecedor
            };

            Requisicao requisicao = new Requisicao
            {
                Id = id,
                Data = data,
                Funcionario = funcionario,
                Medicamento = medicamento,
                QtdMedicamento = QtdMedicamento,
                Paciente = paciente
            };

            return requisicao;
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Requisicao requisicao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", requisicao.Id);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Inserir(Requisicao requisicao)
        {
            var validador = new ValidadorRequisicao();

            var resultadoValidacao = validador.Validate(requisicao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosRequisicao(requisicao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            requisicao.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Requisicao SelecionarPorId(Requisicao requisicao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", requisicao.Id);

            conexaoComBanco.Open();

            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            Requisicao requisicaoSelecionada = null;

            if (leitorRequisicao.Read())
                requisicaoSelecionada = ConverterParaRequisicao(leitorRequisicao);

            conexaoComBanco.Close();

            return requisicaoSelecionada;
        }

        public List<Requisicao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorRequisicao = comandoSelecao.ExecuteReader();

            List<Requisicao> requisicoes = new List<Requisicao>();

            while (leitorRequisicao.Read())
            {
                Requisicao requisicao = ConverterParaRequisicao(leitorRequisicao);

                requisicoes.Add(requisicao);
            }

            conexaoComBanco.Close();

            return requisicoes;
        }
    }
}
