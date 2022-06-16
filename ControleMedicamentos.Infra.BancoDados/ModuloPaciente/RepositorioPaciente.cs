using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPaciente : RepositorioBase<Paciente, ValidadorPaciente, MapeadorPaciente> ,IRepositorioPaciente
    {
        protected override string sqlInserir => 
            @"INSERT INTO TBPACIENTE 
                (
                    [NOME],
                    [CARTAOSUS]
	            )
	            VALUES
                (
                    @NOME,
                    @CARTAOSUS
                );
				SELECT SCOPE_IDENTITY()";

        protected override string sqlEditar =>
            @"UPDATE TBPACIENTE	
                SET
	               [NOME] = @NOME,
                   [CARTAOSUS] = @CARTAOSUS
                WHERE
	                [ID] = @ID;";

        protected override string sqlExcluir =>
            @"DELETE FROM[TBPACIENTE]

                WHERE
                    [ID] = @ID";

        protected override string sqlSelecionarPorId =>
            @"SELECT 
                 PACIENTE.ID,
                 PACIENTE.NOME,
                 PACIENTE.CARTAOSUS

                FROM TBPACIENTE AS PACIENTE;";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
                 PACIENTE.ID,
                 PACIENTE.NOME,
                 PACIENTE.CARTAOSUS

                FROM TBPACIENTE AS PACIENTE;";
    }
}
