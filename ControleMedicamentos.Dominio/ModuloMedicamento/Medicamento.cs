using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using System;
using System.Collections.Generic;

namespace ControleMedicamentos.Dominio.ModuloMedicamento
{
    public class Medicamento : EntidadeBase<Medicamento>
    {        
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Lote { get; set; }
        public DateTime Validade { get; set; }
        private int _quantidadeDisponivel;
        public int QuantidadeDisponivel 
        {
            get { return _quantidadeDisponivel; } 
            private set { _quantidadeDisponivel = value; } 
        }

        private List<Requisicao> Requisicoes { get; set; }

        public Fornecedor Fornecedor{ get; set; }

        public int QuantidadeRequisicoes { get { return Requisicoes.Count; } }

        public Medicamento(string nome, string descricao, string lote, DateTime validade, int quantidadeDisponivel)
        {
            Nome = nome;
            Descricao = descricao;
            Lote = lote;
            Validade = validade;
            QuantidadeDisponivel = quantidadeDisponivel;
            Requisicoes = new List<Requisicao>();
            Fornecedor = new Fornecedor();
        }

        public Medicamento()
        {
            this.Requisicoes = new List<Requisicao>();
            this.Fornecedor = new Fornecedor();
        }

        public void AtualizarRegistro(Medicamento medicamento)
        {
            this.Nome  = medicamento.Nome;
            this.Descricao  = medicamento.Descricao;
            this.Lote  = medicamento.Lote;
            this.Validade  = medicamento.Validade;
            this.Requisicoes = medicamento.Requisicoes;
            this.Fornecedor.AtualizarRegistro(medicamento.Fornecedor);
        }

        public bool BaixarMedicamento(int quantidade)
        {
            if(quantidade < QuantidadeDisponivel)
            {
                QuantidadeDisponivel -= quantidade;
                return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            return obj is Medicamento medicamento &&
                   Nome == medicamento.Nome  &&
                   Descricao == medicamento.Descricao  &&
                   Lote == medicamento.Lote  &&
                   Validade == medicamento.Validade  &&
                   QuantidadeDisponivel == medicamento.QuantidadeDisponivel  &&
                   medicamento.Fornecedor.Equals(Fornecedor);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nome, Descricao, Lote, Validade);
        }

        public override string ToString()
        {
            return $"Id: {Id} | Nome: {Nome} | Descricao: {Descricao} | Lote: {Lote} | Validade: {Validade}";
        }

        public Medicamento Clonar()
        {
            return MemberwiseClone() as Medicamento;
        }

        public bool AdicionarRequisicao(Requisicao requisicao)
        {
            //var existeRequisicaoIgualCadastrada = Requisicoes.FindAll(x => x.Equals(requisicao));

            bool existeRequisicaoIgualCadastrada = false;

            foreach (var item in Requisicoes)
            {
                existeRequisicaoIgualCadastrada = item.Equals(requisicao);
                if (existeRequisicaoIgualCadastrada)
                {
                    break;
                }
            }

            if (!existeRequisicaoIgualCadastrada)
            {
                Requisicoes.Add(requisicao);
            }

            return false;
        }

        public List<Requisicao> VisualizarRequisicoes()
        {
            return Requisicoes;
        }
    }
}
