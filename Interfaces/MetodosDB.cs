using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.Interfaces
{
    internal static class MetodosDB
    {
        private static MySqlConnection conexao()
        {
            try
            {
                MySqlConnection con = new MySqlConnection("Server=localhost;Database=BD_FinTracker;User Id=root;Password=admin");
                con.Open();
                return con;
            }
            catch(Exception erro)
            {
                MessageBox.Show(erro.Message);
            }
            return null;
        }

        public static String executarQuerrySimples(String querry)
        {
            MySqlConnection con = conexao();
            if (con == null)
                return null;
            MySqlCommand command = new MySqlCommand(querry, con);
            var resultado = command.ExecuteScalar();
            if (resultado != null)
                return resultado.ToString();
            return null;
        }
    }
}
