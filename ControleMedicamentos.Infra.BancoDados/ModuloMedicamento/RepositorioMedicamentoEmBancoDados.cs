using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados : RepositorioBase<Medicamento, ValidadorMedicamento, MapeadorMedicamento> ,IRepositorioMedicamento
    {
        protected override string sqlInserir =>
             @"INSERT INTO [TBMEDICAMENTO] 
                (
                    [ID],
                    [NOME],
                    [DESCRICAO],
                    [LOTE],
                    [VALIDADE],
                    [QUANTIDADEDISPONIVEL],
                    [FORNECEDOR_ID]
	            )
	            VALUES
                (
                    @ID,
                    @NOME,
                    @DESCRICAO,
                    @LOTE,
                    @VALIDADE,
                    @QUANTIDADEDISPONIVEL,
                    @FORNECEDOR_ID
                );";

        protected override string sqlEditar =>
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

        protected override string sqlExcluir =>
            @"DELETE FROM[TBMEDICAMENTO]

                WHERE
                    [ID] = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
	                MEDICAMENTO.ID AS MEDICAMENTO_ID,
	                MEDICAMENTO.NOME AS MEDICAMENTO_NOME,
	                MEDICAMENTO.DESCRICAO AS MEDICAMENTO_DESCRICAO,
	                MEDICAMENTO.LOTE AS MEDICAMENTO_LOTE,
	                MEDICAMENTO.VALIDADE AS MEDICAMENTO_VALIDADE,
	                MEDICAMENTO.QUANTIDADEDISPONIVEL AS MEDICAMENTO_QUANTIDADE_DISPONIVEL,
	                FORNECEDOR.ID AS FORNECEDOR_ID,
	                FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                FORNECEDOR.EMAIL as FORNECEDOR_EMAIL,
	                FORNECEDOR.TELEFONE as FORNECEDOR_TELEFONE,
	                FORNECEDOR.ESTADO as FORNECEDOR_ESTADO,
	                FORNECEDOR.CIDADE as FORNECEDOR_CIDADE

                FROM TBMEDICAMENTO AS MEDICAMENTO

                INNER JOIN TBFORNECEDOR AS FORNECEDOR

                ON FORNECEDOR.ID = MEDICAMENTO.FORNCEDOR_ID";

        protected override string sqlSelecionarPorId =>
             @"SELECT 
	                MEDICAMENTO.ID AS MEDICAMENTO_ID,
	                MEDICAMENTO.NOME AS MEDICAMENTO_NOME,
	                MEDICAMENTO.DESCRICAO AS MEDICAMENTO_DESCRICAO,
	                MEDICAMENTO.LOTE AS MEDICAMENTO_LOTE,
	                MEDICAMENTO.VALIDADE AS MEDICAMENTO_VALIDADE,
	                MEDICAMENTO.QUANTIDADEDISPONIVEL AS MEDICAMENTO_QUANTIDADE_DISPONIVEL,
	                FORNECEDOR.ID AS FORNECEDOR_ID,
	                FORNECEDOR.NOME AS FORNECEDOR_NOME,
	                FORNECEDOR.EMAIL as FORNECEDOR_EMAIL,
	                FORNECEDOR.TELEFONE as FORNECEDOR_TELEFONE,
	                FORNECEDOR.ESTADO as FORNECEDOR_ESTADO,
	                FORNECEDOR.CIDADE as FORNECEDOR_CIDADE

                FROM TBMEDICAMENTO AS MEDICAMENTO

                INNER JOIN TBFORNECEDOR AS FORNECEDOR

                ON FORNECEDOR.ID = MEDICAMENTO.FORNECEDOR_ID

                WHERE MEDICAMENTO.ID = @ID";

        private string sqlRequisicoesPorMedicamento =>
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
	                    MEDICAMENTO.QUANTIDADEDISPONIVEL AS MEDICAMENTO_QUANTIDADE_DISPONIVEL,

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

                    WHERE MEDICAMENTO.ID = @ID";

        public Medicamento SelecionarRequisicoesMedicamento(Medicamento medicamento)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlRequisicoesPorMedicamento, conexaoComBanco);
            
            comandoSelecao.Parameters.AddWithValue("ID", medicamento.Id);
            
            conexaoComBanco.Open();

            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            var mapeador = new MapeadorRequisicao();

            while (leitorPaciente.Read())
                medicamento.AdicionarRequisicao(mapeador.ConverterRegistro(leitorPaciente));

            conexaoComBanco.Close();

            
            return medicamento;
        }
    }
}
