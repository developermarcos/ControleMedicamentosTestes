using ControleMedicamentos.Dominio.ModuloPaciente;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class RepositorioPaciente : IRepositorioPaciente
    {
        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=ControleMedicamentoDb;" +
              "Integrated Security=True;" +
              "Pooling=False";
        public string sqlInserir =>
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

        public string sqlEditar =>
             @"UPDATE TBPACIENTE	
                SET
	                [NOME] = @NOME,
                    [CARTAOSUS] = @CARTAOSUS
                WHERE
	                [ID] = @ID;";

        public string sqlExcluir =>
            @"DELETE FROM[TBPACIENTE]

                WHERE
                    [ID] = @ID";

        public string sqlSelecionarTodos =>
            @"SELECT 
	                PACIENTE.ID,
	                PACIENTE.NOME,
	                PACIENTE.CARTAOSUS

                FROM TBPACIENTE AS PACIENTE;";

        public string sqlSelecionarPorId =>
            @"SELECT 
	                PACIENTE.ID,
	                PACIENTE.NOME,
	                PACIENTE.CARTAOSUS

                FROM TBPACIENTE AS PACIENTE

                WHERE ID = @ID";

        public void ConfigurarParametrosPaciente(Paciente novoPaciente, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoPaciente.Id);
            comando.Parameters.AddWithValue("NOME", novoPaciente.Nome);
            comando.Parameters.AddWithValue("CARTAOSUS", novoPaciente.CartaoSUS);
        }

        public Paciente ConverterParaPaciente(SqlDataReader leitorPaciente)
        {
            int id = Convert.ToInt32(leitorPaciente["ID"]);
            string nome = Convert.ToString(leitorPaciente["NOME"]);
            string cartaosus = Convert.ToString(leitorPaciente["CARTAOSUS"]);
            
            var paciente = new Paciente
            {
                Id = id,
                Nome = nome,
                CartaoSUS = cartaosus
            };

            return paciente;
        }

        public ValidationResult Editar(Paciente paciente)
        {
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosPaciente(paciente, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Paciente paciente)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", paciente.Id);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Inserir(Paciente paciente)
        {
            var validador = new ValidadorPaciente();

            var resultadoValidacao = validador.Validate(paciente);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosPaciente(paciente, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            paciente.Id = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public Paciente SelecionarPorId(Paciente paciente)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", paciente.Id);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            Paciente pacienteSelecionado = null;

            if (leitorPaciente.Read())
                pacienteSelecionado = ConverterParaPaciente(leitorPaciente);

            conexaoComBanco.Close();

            return pacienteSelecionado;
        }

        public List<Paciente> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            List<Paciente> pacientes = new List<Paciente>();

            while (leitorPaciente.Read())
            {
                Paciente paciente = ConverterParaPaciente(leitorPaciente);

                pacientes.Add(paciente);
            }

            conexaoComBanco.Close();

            return pacientes;
        }
    }
}
