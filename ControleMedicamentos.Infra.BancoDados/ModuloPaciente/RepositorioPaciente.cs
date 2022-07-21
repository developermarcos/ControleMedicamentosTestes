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
                    [ID],
                    [NOME],
                    [CARTAOSUS]
	            )
	            VALUES
                (
                    @ID,
                    @NOME,
                    @CARTAOSUS
                );";

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
                 PACIENTE.ID AS PACIENTE_ID,
                 PACIENTE.NOME AS PACIENTE_NOME,
                 PACIENTE.CARTAOSUS AS PACIENTE_CARTAOSUS

                FROM TBPACIENTE AS PACIENTE;";

        protected override string sqlSelecionarTodos =>
            @"SELECT 
                 PACIENTE.ID AS PACIENTE_ID,
                 PACIENTE.NOME AS PACIENTE_NOME,
                 PACIENTE.CARTAOSUS AS PACIENTE_CARTAOSUS

                FROM TBPACIENTE AS PACIENTE;";
    }
}
