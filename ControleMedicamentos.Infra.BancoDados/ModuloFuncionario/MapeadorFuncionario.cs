
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFuncionario
{
    public class MapeadorFuncionario : MapeadorBase<Funcionario>
    {
        public override void ConfigurarParametros(Funcionario registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("LOGIN", registro.Login);
            comando.Parameters.AddWithValue("SENHA", registro.Senha);
        }

        public override Funcionario ConverterRegistro(SqlDataReader leitorRegistro)
        {
            Guid id = Guid.Parse(leitorRegistro["FUNCIONARIO_ID"].ToString());
            string nome = Convert.ToString(leitorRegistro["FUNCIONARIO_NOME"]);
            string login = Convert.ToString(leitorRegistro["FUNCIONARIO_LOGIN"]);
            string senha = Convert.ToString(leitorRegistro["FUNCIONARIO_SENHA"]);

            var funcionario = new Funcionario
            {
                Id = id,
                Nome = nome,
                Login = login,
                Senha = senha
            };

            return funcionario;
        }
    }
}
