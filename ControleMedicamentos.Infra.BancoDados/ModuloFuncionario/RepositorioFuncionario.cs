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
                    [ID],
                    [NOME],
                    [LOGIN],
                    [SENHA]
	            )
	            VALUES
                (
                    @ID,
                    @NOME,
                    @LOGIN,
                    @SENHA
                );";

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
	                FUNCIONARIO.ID as FUNCIONARIO_ID,
	                FUNCIONARIO.NOME as FUNCIONARIO_NOME,
	                FUNCIONARIO.LOGIN as FUNCIONARIO_LOGIN,
	                FUNCIONARIO.SENHA as FUNCIONARIO_SENHA

                FROM TBFUNCIONARIO AS FUNCIONARIO

                WHERE ID = @ID";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
	                FUNCIONARIO.ID as FUNCIONARIO_ID,
	                FUNCIONARIO.NOME as FUNCIONARIO_NOME,
	                FUNCIONARIO.LOGIN as FUNCIONARIO_LOGIN,
	                FUNCIONARIO.SENHA as FUNCIONARIO_SENHA

                FROM TBFUNCIONARIO AS FUNCIONARIO;";
    }
}
