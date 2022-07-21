using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloMedicamento
{
    public class MapeadorMedicamento : MapeadorBase<Medicamento>
    {
        MapeadorFornecedor mapeadorFornecedor;
        public MapeadorMedicamento() 
        {
            mapeadorFornecedor = new MapeadorFornecedor();
        }
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
            Guid id = Guid.Parse(leitorRegistro["MEDICAMENTO_ID"].ToString());
            string nome = Convert.ToString(leitorRegistro["MEDICAMENTO_NOME"]);
            string login = Convert.ToString(leitorRegistro["MEDICAMENTO_DESCRICAO"]);
            string lote = Convert.ToString(leitorRegistro["MEDICAMENTO_LOTE"]);
            DateTime validade = Convert.ToDateTime(leitorRegistro["MEDICAMENTO_VALIDADE"]);
            int quantidadeDisponivel = Convert.ToInt32(leitorRegistro["MEDICAMENTO_QUANTIDADE_DISPONIVEL"]);

            var fornecedor = mapeadorFornecedor.ConverterRegistro(leitorRegistro);

            var funcionario = new Medicamento(nome, login, lote, validade, quantidadeDisponivel);

            funcionario.Id = id;
            funcionario.Fornecedor = fornecedor;



            return funcionario;
        }
    }
}
