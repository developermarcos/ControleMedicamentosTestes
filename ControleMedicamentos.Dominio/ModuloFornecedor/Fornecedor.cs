using ControleMedicamentos.Dominio;

namespace ControleFornecedors.Dominio.ModuloFornecedor
{
    public class Fornecedor : EntidadeBase<Fornecedor>
    {
        public Fornecedor()
        {
        }

        public Fornecedor(string nome, string telefone, string email, string cidade, string estado)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Cidade = cidade;
            Estado = estado;
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public void AtualizarRegistro(Fornecedor forn)
        {
            this.Nome = forn.Nome;
            this.Telefone = forn.Telefone;
            this.Email = forn.Email;
            this.Cidade = forn.Cidade;
            this.Estado = forn.Estado;
        }

        public override string ToString()
        {
            return $"Numero: {Id} | Nome: {Nome} | Telefone: {Telefone}";
        }
        public override bool Equals(object obj)
        {
            return obj is Fornecedor Fornecedor &&
                   Nome == Fornecedor.Nome  &&
                   Telefone == Fornecedor.Telefone  &&
                   Email == Fornecedor.Email  &&
                   Cidade == Fornecedor.Cidade  &&
                   Estado == Fornecedor.Estado;
        }
    }
}
