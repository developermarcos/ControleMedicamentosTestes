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
            Guid id = Guid.Parse(leitorRegistro["FORNECEDOR_ID"].ToString());
            string nome = Convert.ToString(leitorRegistro["FORNECEDOR_NOME"]);
            string email = Convert.ToString(leitorRegistro["FORNECEDOR_EMAIL"]);
            string telefone = Convert.ToString(leitorRegistro["FORNECEDOR_TELEFONE"]);
            string cidade = Convert.ToString(leitorRegistro["FORNECEDOR_CIDADE"]);
            string estado = Convert.ToString(leitorRegistro["FORNECEDOR_ESTADO"]);

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
