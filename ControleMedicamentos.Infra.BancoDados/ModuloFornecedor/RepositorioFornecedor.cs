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
	                FORNECEDOR.ID,
	                FORNECEDOR.NOME,
	                FORNECEDOR.TELEFONE,
	                FORNECEDOR.EMAIL,
	                FORNECEDOR.CIDADE,
	                FORNECEDOR.ESTADO
                
                FROM TBFORNECEDOR AS FORNECEDOR

                WHERE ID = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT
	                FORNECEDOR.ID,
	                FORNECEDOR.NOME,
	                FORNECEDOR.TELEFONE,
	                FORNECEDOR.EMAIL,
	                FORNECEDOR.CIDADE,
	                FORNECEDOR.ESTADO
                
                FROM TBFORNECEDOR AS FORNECEDOR";
    }
}
