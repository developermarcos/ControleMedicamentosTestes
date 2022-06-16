using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using System;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class Requisicao : EntidadeBase<Requisicao>
    {
        public Requisicao() { }
        public Requisicao(Medicamento medicamento, Paciente paciente, int qtdMedicamento, DateTime data, Funcionario funcionario)
        {
            Medicamento=medicamento;
            Paciente=paciente;
            QtdMedicamento=qtdMedicamento;
            Data=data;
            Funcionario=funcionario;
        }

        public Medicamento Medicamento { get; set; }
        public Paciente Paciente { get; set; }

        private int _qtdMedicamento;
        public int QtdMedicamento 
        {   
            get
            { 
                return _qtdMedicamento; 
            }
            set
            {
                _qtdMedicamento = value > Medicamento.QuantidadeDisponivel ? Medicamento.QuantidadeDisponivel : value;
            } 
        }
        public DateTime Data { get; set; }
        public Funcionario Funcionario { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Requisicao requisicao &&
                   requisicao.Medicamento.Equals(Medicamento)  &&
                   requisicao.Paciente.Equals(Paciente)  &&
                   QtdMedicamento == requisicao.QtdMedicamento  &&
                   Data == requisicao.Data  &&
                   requisicao.Funcionario.Equals(Funcionario);
        }

        public override string ToString()
        {
            return $"ID: {Id} | Data: {Data} | Funcionario: {Funcionario.Nome} | Medicamento: {Medicamento.Nome} | Paciente: {Paciente.Nome}";
        }

        public void AtualizarRequisicao(Requisicao requisicao)
        {
            this.QtdMedicamento=requisicao.QtdMedicamento;
            this.Data=requisicao.Data;
            this.Funcionario.AtualizarRegistro(requisicao.Funcionario);
            this.Paciente.AtualizarRegistro(requisicao.Paciente);
            this.Medicamento.AtualizarRegistro(requisicao.Medicamento);
        }

    }
}
