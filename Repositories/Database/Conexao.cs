using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Database
{
    class Conexao
    {
        private static readonly string ConnectionString;
        
        static Conexao()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public static SqlCommand ObterConexao()
        {
            SqlConnection conexao = new SqlConnection(ConnectionString);
            conexao.Open();

            SqlCommand comando = new SqlCommand();
            return comando;
        }
    }
}
