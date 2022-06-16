using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloMedicamento
{
    public class MapeadorMedicamento : MapeadorBase<Medicamento>
    {
        public override void ConfigurarParametros(Medicamento registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("DESCRICAO", registro.Descricao);
            comando.Parameters.AddWithValue("LOTE", registro.Lote);
            comando.Parameters.AddWithValue("VALIDADE", registro.Validade);
            comando.Parameters.AddWithValue("QUANTIDADEDISPONIVEL", registro.QuantidadeDisponivel);
            comando.Parameters.AddWithValue("FORNECEDOR_ID", registro.Fornecedor.Id);
        }

        public override Medicamento ConverterRegistro(SqlDataReader leitorRegistro)
        {
            int id = Convert.ToInt32(leitorRegistro["ID"]);
            string nome = Convert.ToString(leitorRegistro["NOME"]);
            string login = Convert.ToString(leitorRegistro["DESCRICAO"]);
            string lote = Convert.ToString(leitorRegistro["LOTE"]);
            DateTime validade = Convert.ToDateTime(leitorRegistro["VALIDADE"]);
            int quantidadeDisponivel = Convert.ToInt32(leitorRegistro["QUANTIDADEDISPONIVEL"]);
            int fornecedor_id = Convert.ToInt32(leitorRegistro["FORNECEDOR_ID"]);
            string fornecedor_nome = Convert.ToString(leitorRegistro["FORNECEDOR_NOME"]);
            string fornecedor_telefone = Convert.ToString(leitorRegistro["TELEFONE"]);
            string fornecedor_email = Convert.ToString(leitorRegistro["EMAIL"]);
            string fornecedor_cidade = Convert.ToString(leitorRegistro["CIDADE"]);
            string fornecedor_estado = Convert.ToString(leitorRegistro["ESTADO"]);

            var fornecedor = new Fornecedor
            {
                Id = fornecedor_id,
                Nome = fornecedor_nome,
                Telefone = fornecedor_telefone,
                Email = fornecedor_email,
                Cidade = fornecedor_cidade,
                Estado = fornecedor_estado
            };

            var funcionario = new Medicamento(nome, login, lote, validade, quantidadeDisponivel);

            funcionario.Id = id;
            funcionario.Fornecedor = fornecedor;



            return funcionario;
        }
    }
}
