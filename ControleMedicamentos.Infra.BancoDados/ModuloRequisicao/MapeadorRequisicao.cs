
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class MapeadorRequisicao : MapeadorBase<Requisicao>
    {
        public override void ConfigurarParametros(Requisicao registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("QUANTIDADEMEDICAMENTO", registro.QtdMedicamento);
            comando.Parameters.AddWithValue("DATA", registro.Data);
            comando.Parameters.AddWithValue("FUNCIONARIO_ID", registro.Funcionario.Id);
            comando.Parameters.AddWithValue("PACIENTE_ID", registro.Paciente.Id);
            comando.Parameters.AddWithValue("MEDICAMENTO_ID", registro.Medicamento.Id);
        }

        public override Requisicao ConverterRegistro(SqlDataReader leitorRegistro)
        {
            int id = Convert.ToInt32(leitorRegistro["ID"]);
            int QtdMedicamento = Convert.ToInt32(leitorRegistro["QUANTIDADEMEDICAMENTO"]);
            DateTime data = Convert.ToDateTime(leitorRegistro["DATA"]);

            int funcionario_id = Convert.ToInt32(leitorRegistro["FUNCIONARIO_ID"]);
            string funcionario_nome = Convert.ToString(leitorRegistro["FUNCIONARIO_NOME"]);
            string funcionario_login = Convert.ToString(leitorRegistro["FUNCIONARIO_LOGIN"]);
            string funcionario_senha = Convert.ToString(leitorRegistro["FUNCIONARIO_SENHA"]);

            int paciente_id = Convert.ToInt32(leitorRegistro["PACIENTE_ID"]);
            string paciente_nome = Convert.ToString(leitorRegistro["PACIENTE_NOME"]);
            string paciente_cartaosus = Convert.ToString(leitorRegistro["PACIENTE_CARTAOSUS"]);

            int medicamento_id = Convert.ToInt32(leitorRegistro["MEDICAMENTO_ID"]);
            string medicamento_nome = Convert.ToString(leitorRegistro["MEDICAMENTO_NOME"]);
            string medicamento_descricao = Convert.ToString(leitorRegistro["MEDICAMENTO_DESCRICAO"]);
            string medicamento_lote = Convert.ToString(leitorRegistro["MEDICAMENTO_LOTE"]);
            DateTime medicamento_validade = Convert.ToDateTime(leitorRegistro["MEDICAMENTO_VALIDADE"]);
            int medicamento_qtddisponivel = Convert.ToInt32(leitorRegistro["MEDICAMENTO_QTDDISPONIVEL"]);

            int Fornecedor_id = Convert.ToInt32(leitorRegistro["FORNECEDOR_ID"]);
            string Fornecedor_nome = Convert.ToString(leitorRegistro["FORNECEDOR_NOME"]);
            string Fornecedor_email = Convert.ToString(leitorRegistro["FORNECEDOR_EMAIL"]);
            string Fornecedor_telefone = Convert.ToString(leitorRegistro["FORNECEDOR_TELEFONE"]);
            string Fornecedor_cidade = Convert.ToString(leitorRegistro["FORNECEDOR_CIDADE"]);
            string Fornecedor_estado = Convert.ToString(leitorRegistro["FORNECEDOR_ESTADO"]);

            var fornecedor = new Fornecedor
            {
                Id = Fornecedor_id,
                Nome = Fornecedor_nome,
                Email = Fornecedor_email,
                Telefone = Fornecedor_telefone,
                Cidade = Fornecedor_cidade,
                Estado = Fornecedor_estado
            };

            var paciente = new Paciente
            {
                Id = paciente_id,
                Nome = paciente_nome,
                CartaoSUS = paciente_cartaosus
            };

            var funcionario = new Funcionario
            {
                Id = funcionario_id,
                Nome = funcionario_nome,
                Login = funcionario_login,
                Senha = funcionario_senha
            };

            var medicamento = new Medicamento
            {
                Id = medicamento_id,
                Nome = medicamento_nome,
                Descricao = medicamento_descricao,
                Lote = medicamento_lote,
                Validade = medicamento_validade,
                QuantidadeDisponivel = medicamento_qtddisponivel,
                Fornecedor = fornecedor
            };

            Requisicao requisicao = new Requisicao
            {
                Id = id,
                Data = data,
                Funcionario = funcionario,
                Medicamento = medicamento,
                QtdMedicamento = QtdMedicamento,
                Paciente = paciente
            };

            return requisicao;
        }
    }
}
