using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class RepositorioFornecedor : RepositorioBase<Fornecedor, ValidadorFornecedor, MapeadorFornecedor>, IRepositorioFornecedor
    {
        
        protected override string sqlInserir =>
            @"INSERT INTO TBFORNECEDOR
                (
                    [ID],
                    [NOME],
                    [EMAIL],
                    [TELEFONE],
                    [CIDADE],
                    [ESTADO]
	            )
	            VALUES
                (
                    @ID,
                    @NOME,
                    @EMAIL,
                    @TELEFONE,
                    @CIDADE,
                    @ESTADO
                );";

        protected override string sqlEditar =>
            @"UPDATE TBFORNECEDOR	
                SET
	                [NOME] = @NOME,
                    [EMAIL] = @EMAIL,
                    [TELEFONE] = @TELEFONE,
                    [CIDADE] = @CIDADE,
                    [ESTADO] = @ESTADO
                WHERE
	                [ID] = @ID;";

        protected override string sqlExcluir =>
            @"DELETE FROM[TBFORNECEDOR]

                WHERE
                    [ID] = @ID";

        protected override string sqlSelecionarPorId =>
            @"SELECT
	                FORNECEDOR.ID as FORNECEDOR_ID,
	                FORNECEDOR.NOME as FORNECEDOR_NOME,
	                FORNECEDOR.TELEFONE as FORNECEDOR_TELEFONE,
	                FORNECEDOR.EMAIL as FORNECEDOR_EMAIL,
	                FORNECEDOR.CIDADE as FORNECEDOR_CIDADE,
	                FORNECEDOR.ESTADO as FORNECEDOR_ESTADO
                
                FROM TBFORNECEDOR AS FORNECEDOR

                WHERE ID = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT
	                FORNECEDOR.ID as FORNECEDOR_ID,
	                FORNECEDOR.NOME as FORNECEDOR_NOME,
	                FORNECEDOR.TELEFONE as FORNECEDOR_TELEFONE,
	                FORNECEDOR.EMAIL as FORNECEDOR_EMAIL,
	                FORNECEDOR.CIDADE as FORNECEDOR_CIDADE,
	                FORNECEDOR.ESTADO as FORNECEDOR_ESTADO
                
                FROM TBFORNECEDOR AS FORNECEDOR";
    }
}
