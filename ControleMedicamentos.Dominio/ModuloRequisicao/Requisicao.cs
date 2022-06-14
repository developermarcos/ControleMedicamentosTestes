using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloRequisicao
{
    public class Requisicao : EntidadeBase<Requisicao>
    {
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
        public int QtdMedicamento { get; set; }
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

    }
}
