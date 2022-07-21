
using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using System;
using System.Data.SqlClient;

namespace ControleMedicamentos.Infra.BancoDados.ModuloRequisicao
{
    public class MapeadorRequisicao : MapeadorBase<Requisicao>
    {
        MapeadorFornecedor mapeadorFornecedor;
        MapeadorFuncionario mapeadorFuncionario;
        MapeadorMedicamento mapeadorMedicamento;
        MapeadorPaciente mapeadorPaciente;

        public MapeadorRequisicao()
        {
            this.mapeadorFornecedor= new MapeadorFornecedor();
            this.mapeadorFuncionario= new MapeadorFuncionario();
            this.mapeadorMedicamento= new MapeadorMedicamento();
            this.mapeadorPaciente= new MapeadorPaciente();
        }

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
            Guid id = Guid.Parse(leitorRegistro["REQUISICAO_ID"].ToString());
            int QtdMedicamento = Convert.ToInt32(leitorRegistro["REQUISICAO_QUANTIDADE_MEDICAMENTO"]);
            DateTime data = Convert.ToDateTime(leitorRegistro["REQUISICAO_DATA"]);

            var funcionario = mapeadorFuncionario.ConverterRegistro(leitorRegistro);
            var paciente = mapeadorPaciente.ConverterRegistro(leitorRegistro);
            var medicamento = mapeadorMedicamento.ConverterRegistro(leitorRegistro);
            var fornecedor = mapeadorFornecedor.ConverterRegistro(leitorRegistro);
            
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
