using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class RepositorioFuncionario : RepositorioBase<Funcionario, ValidadorFuncionario, MapeadorFuncionario> ,IRepositorioFuncionario
    {
        protected override string sqlInserir =>
             @"INSERT INTO TBFUNCIONARIO 
                (
                    [NOME],
                    [LOGIN],
                    [SENHA]
	            )
	            VALUES
                (
                    @NOME,
                    @LOGIN,
                    @SENHA
                );
				SELECT SCOPE_IDENTITY()";

        protected override string sqlEditar =>
            @"UPDATE TBFUNCIONARIO	
                SET
	                [NOME] = @NOME,
	                [LOGIN] = @LOGIN,
	                [SENHA] = @SENHA
                WHERE
	                [ID] = @ID;";

        protected override string sqlExcluir =>
            @"DELETE FROM[TBFUNCIONARIO]

                WHERE
                    [ID] = @ID";
        
        protected override string sqlSelecionarPorId =>
            @"SELECT 
	                FUNCIONARIO.ID,
	                FUNCIONARIO.NOME,
	                FUNCIONARIO.LOGIN,
	                FUNCIONARIO.SENHA

                FROM TBFUNCIONARIO AS FUNCIONARIO

                WHERE ID = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
	                FUNCIONARIO.ID,
	                FUNCIONARIO.NOME,
	                FUNCIONARIO.LOGIN,
	                FUNCIONARIO.SENHA

                FROM TBFUNCIONARIO AS FUNCIONARIO;";
    }
}
