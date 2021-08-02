using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agendaFacilAPI.Database
{
    public class BuscarConexaoBD
    {
        public static string BuscarConexao()
        {
            return ConfigurationManager.ConnectionStrings["conexaoIbitirama"].ConnectionString;
        }
    }
}
