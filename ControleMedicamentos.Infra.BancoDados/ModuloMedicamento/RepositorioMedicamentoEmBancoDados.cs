using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;

namespace ControleMedicamento.Infra.BancoDados.ModuloMedicamento
{
    public class RepositorioMedicamentoEmBancoDados : RepositorioBase<Medicamento, ValidadorMedicamento, MapeadorMedicamento> ,IRepositorioMedicamento
    {
        protected override string sqlInserir =>
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

        protected override string sqlSelecionarPorId =>
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

    }
}
