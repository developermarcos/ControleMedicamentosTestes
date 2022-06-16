
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

        public string sqlEditar => @"";

        public string sqlExcluir => @"";

        public string sqlSelecionarTodos => @"";

        public string sqlSelecionarPorId => @"";

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

            int medicamento_id = Convert.ToInt32(leitorRequisicao["PACIENTE_ID"]);
            string medicamento_nome = Convert.ToString(leitorRequisicao["PACIENTE_NOME"]);
            string medicamento_descricao = Convert.ToString(leitorRequisicao["PACIENTE_CARTAOSUS"]);
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
                QtdMedicamento = QtdMedicamento,
                Funcionario = funcionario,
                Medicamento = medicamento,
                Paciente = paciente
            };

            return requisicao;
        }

        public ValidationResult Editar(Requisicao requisicao)
        {
            throw new System.NotImplementedException();
        }

        public ValidationResult Excluir(Requisicao requisicao)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public List<Requisicao> SelecionarTodos()
        {
            throw new System.NotImplementedException();
        }
    }
}
