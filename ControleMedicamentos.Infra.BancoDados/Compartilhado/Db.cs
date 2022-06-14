using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.Compartilhado
{
    public static class Db
    {
        private const string enderecoBanco =
            "Data Source=(LocalDb)\\MSSQLLocalDB;" +
            "Initial Catalog=ControleMedicamentoDb;" +
            "Integrated Security=True;" +
            "Pooling=False";

        public static void ExecutarSql(string sql)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comando = new SqlCommand(sql, conexaoComBanco);

            conexaoComBanco.Open();
            comando.ExecuteNonQuery();
            conexaoComBanco.Close();
        }
    }
}
