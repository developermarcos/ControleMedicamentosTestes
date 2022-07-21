using ControleMedicamentos.Dominio;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public abstract class RepositorioBase<T, TValidador, TMapeador>
        where T : EntidadeBase<T>
        where TValidador : AbstractValidator<T>, new()
        where TMapeador : MapeadorBase<T>, new()
    {
        protected string enderecoBanco =
            "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=ControleMedicamentoDb;" +
              "Integrated Security=True;" +
              "Pooling=False";
        protected abstract string sqlInserir { get; }

        protected abstract string sqlEditar { get; }

        protected abstract string sqlExcluir { get; }

        protected abstract string sqlSelecionarPorId { get; }

        protected abstract string sqlSelecionarTodos { get; }

        public ValidationResult Inserir(T registro)
        {
            var validador = new TValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            var mapeador = new TMapeador();

            mapeador.ConfigurarParametros(registro, comandoInsercao);

            conexaoComBanco.Open();
            
            comandoInsercao.ExecuteNonQuery();
            
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Editar(T registro)
        {
            var validador = new TValidador();

            var resultadoValidacao = validador.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            var mapeador = new TMapeador();

            mapeador.ConfigurarParametros(registro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(T registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();
            conexaoComBanco.Close();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public T SelecionarPorId(T registro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorId, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", registro.Id);

            conexaoComBanco.Open();
            SqlDataReader leitorRegistro = comandoSelecao.ExecuteReader();

            var mapeador = new TMapeador();
            T registroBuscar = null;
            if (leitorRegistro.Read())
                registroBuscar = mapeador.ConverterRegistro(leitorRegistro);

            conexaoComBanco.Close();

            return registroBuscar;
        }

        public List<T> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);
            conexaoComBanco.Open();

            SqlDataReader leitorPaciente = comandoSelecao.ExecuteReader();

            var mapeador = new TMapeador();

            List<T> registros = new List<T>();

            while (leitorPaciente.Read())
                registros.Add(mapeador.ConverterRegistro(leitorPaciente));

            conexaoComBanco.Close();

            return registros;
        }
    }
}
