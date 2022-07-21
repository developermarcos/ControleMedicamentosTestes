using ControleMedicamentos.Dominio.ModuloPaciente;
using ControleMedicamentos.Infra.BancoDados.Compartilhado;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleMedicamentos.Infra.BancoDados.ModuloPaciente
{
    public class MapeadorPaciente : MapeadorBase<Paciente>
    {
        public override void ConfigurarParametros(Paciente registro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", registro.Id);
            comando.Parameters.AddWithValue("NOME", registro.Nome);
            comando.Parameters.AddWithValue("CARTAOSUS", registro.CartaoSUS);
        }

        public override Paciente ConverterRegistro(SqlDataReader leitorRegistro)
        {
            Guid id = Guid.Parse(leitorRegistro["PACIENTE_ID"].ToString());
            string nome = Convert.ToString(leitorRegistro["PACIENTE_NOME"]);
            string cartaosus = Convert.ToString(leitorRegistro["PACIENTE_CARTAOSUS"]);

            var paciente = new Paciente
            {
                Id = id,
                Nome = nome,
                CartaoSUS = cartaosus
            };

            return paciente;
        }
    }
}
