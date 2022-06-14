namespace ControleMedicamentos.Dominio.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {
        public Funcionario()
        {
        }

        public Funcionario(string nome, string login, string senha)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public void AtualizarRegistro(Funcionario func)
        {
            this.Nome = func.Nome;
            this.Login = func.Login;
            this.Senha = func.Senha;
        }
        public override string ToString()
        {
            return $"Id: {Id} | Nome: {Nome} | Login: {Login}";
        }

        public override bool Equals(object obj)
        {
            return obj is Funcionario funcionario &&
                  Nome == funcionario.Nome  &&
                  Login == funcionario.Login &&
                  Senha == funcionario.Senha;
        }
    }
}
