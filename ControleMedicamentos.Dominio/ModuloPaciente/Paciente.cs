namespace ControleMedicamentos.Dominio.ModuloPaciente
{
    public class Paciente : EntidadeBase<Paciente>
    {
        public Paciente()
        {
        }

        public Paciente(string nome, string cartaoSUS)
        {
            Nome = nome;
            CartaoSUS = cartaoSUS;
        }

        public string Nome { get; set; }
        public string CartaoSUS { get; set; }

        public  void AtualizarRegistro(Paciente paciente)
        {
            this.Nome = paciente.Nome;
            this.CartaoSUS = paciente.CartaoSUS;
        }

        public override string ToString()
        {
            return $"Id: {Id} | Nome: {Nome} | CartaoSUS: {CartaoSUS}";
        }

        public override bool Equals(object obj)
        {
            return obj is Paciente paciente &&
                   Nome == paciente.Nome  &&
                   CartaoSUS == paciente.CartaoSUS;
        }

    }
}
