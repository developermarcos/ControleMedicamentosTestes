
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleRequisicaos.Dominio.ModuloRequisicao;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class RepositorioRequisicaoEmBancoDados : RepositorioBase<Requisicao, ValidadorRequisicao, MapeadorRequisicao> ,IRepositorioRequisicao
    {
        
        protected override string sqlInserir =>
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

        protected override string sqlEditar =>
            @"UPDATE [TBREQUISICAO]	
                SET
	                DATA = @DATA,
                    QUANTIDADEMEDICAMENTO = @QUANTIDADEMEDICAMENTO,
                    FUNCIONARIO_ID = @FUNCIONARIO_ID,
                    PACIENTE_ID = @PACIENTE_ID,
                    MEDICAMENTO_ID = @MEDICAMENTO_ID
                WHERE
	                [ID] = @ID;";

        protected override string sqlExcluir =>
            @"DELETE FROM[TBREQUISICAO]

                WHERE
                    [ID] = @ID";

        protected override string sqlSelecionarPorId =>
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

        protected override string sqlSelecionarTodos =>
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
    }
}
