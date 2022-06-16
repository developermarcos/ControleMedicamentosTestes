using ControleFornecedors.Dominio.ModuloFornecedor;
using ControleMedicamento.Infra.BancoDados.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloFornecedor;
using ControleMedicamentos.Dominio.ModuloFuncionario;
using ControleMedicamentos.Dominio.ModuloMedicamento;
using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Dominio.ModuloRequisicao;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using ControleMedicamentos.Infra.BancoDados.ModuloFornecedor;
using ControleMedicamentos.Infra.BancoDados.ModuloFuncionario;
using ControleMedicamentos.Infra.BancoDados.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.ModuloRequisicao;
using ControleRequisicaos.Dominio.ModuloRequisicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.Tests.Compartilhado
{
    public class BaseTest
    {
        protected IRepositorioPaciente repositorioPaciente;
        protected IRepositorioFornecedor repositorioFornecedor;
        protected IRepositorioFuncionario repositorioFuncionario;
        protected IRepositorioMedicamento repositorioMedicamento;
        protected IRepositorioRequisicao repositorioRequisicao;
        public BaseTest()
        {
            string sql =
                @"DELETE FROM TBREQUISICAO;
                  DBCC CHECKIDENT (TBREQUISICAO, RESEED, 0)

                DELETE FROM TBMEDICAMENTO;
                  DBCC CHECKIDENT (TBMEDICAMENTO, RESEED, 0)

                DELETE FROM TBFORNECEDOR;
                  DBCC CHECKIDENT (TBFORNECEDOR, RESEED, 0)
                
                DELETE FROM TBPACIENTE;
                  DBCC CHECKIDENT (TBPACIENTE, RESEED, 0)

                DELETE FROM TBFUNCIONARIO;
                  DBCC CHECKIDENT (TBFUNCIONARIO, RESEED, 0)";

            Db.ExecutarSql(sql);

            repositorioPaciente = new RepositorioPaciente();
            repositorioFornecedor = new RepositorioFornecedor();
            repositorioFuncionario = new RepositorioFuncionario();
            repositorioMedicamento =  new RepositorioMedicamentoEmBancoDados();
            repositorioRequisicao = new RepositorioRequisicaoEmBancoDados();
        }
        public Paciente ObterPaciente()
        {
            return new Paciente("Paciente 1", "123456789123456");
        }
        public Fornecedor ObterFornecedor()
        {
            return new Fornecedor("Nome fornecedor", "55 49 9999-0000", "teste@teste.com", "cidade", "estado");
        }
        public Funcionario ObterFuncionario()
        {
            return new Funcionario("Nome funcionario", "admin1", "admin1");
        }
        public Medicamento ObterMedicamento()
        {
            var medicamento = new Medicamento("Nome medicamento", "descricao medicamento", "123", new DateTime(2022, 10, 10), 4);
            medicamento.Fornecedor = ObterFornecedor();
            return medicamento;
        }
        public Requisicao ObterRequisicao()
        {
            return new Requisicao(ObterMedicamento(), ObterPaciente(), 3, new DateTime(2022, 10, 10), ObterFuncionario());
        }
    }
}
