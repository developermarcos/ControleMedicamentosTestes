using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloFornecedor
{
    public class MapeadorFornecedor : MapeadorBase<Fornecedor>
    {
        public override void ConfigurarParametros(Fornecedor registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("EMAIL", registro.Email);
            comando.Parameters.AddWithValue("TELEFONE", registro.Telefone);
            comando.Parameters.AddWithValue("CIDADE", registro.Cidade);
            comando.Parameters.AddWithValue("ESTADO", registro.Estado);
        }

        public override Fornecedor ConverterRegistro(SqlDataReader leitorRegistro)
        {
            int id = Convert.ToInt32(leitorRegistro["ID"]);
            string nome = Convert.ToString(leitorRegistro["NOME"]);
            string email = Convert.ToString(leitorRegistro["EMAIL"]);
            string telefone = Convert.ToString(leitorRegistro["TELEFONE"]);
            string cidade = Convert.ToString(leitorRegistro["CIDADE"]);
            string estado = Convert.ToString(leitorRegistro["ESTADO"]);

            var funcionario = new Fornecedor
            {
                Id = id,
                Nome = nome,
                Email = email,
                Telefone = telefone,
                Cidade = cidade,
                Estado = estado
            };

            return funcionario;
        }
    }
}
