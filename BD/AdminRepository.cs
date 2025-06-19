using FinTracker.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.BD
{
    public class AdminRepository
    {
        private string _connectionString;

        public AdminRepository()
        {
        }

        public void AddAdmin(string nome, string email, string senha)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Admin (Nome, Email, Senha) VALUES (@Nome, @Email, @Senha)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAdmins()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Admin", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public async Task<bool> UpdateAdmin(int idAdmin, string nome, string email, string senha)
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                string query = "UPDATE Admin SET Nome = @Nome, Email = @Email, Senha = @Senha WHERE id_Admin = @idAdmin";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idAdmin", idAdmin);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Senha", senha);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Atualizado com sucesso.");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao atualizar" + e.Message);
            }
            return false;
        }

        public async Task<bool> DeleteAdmin(int idAdmin)
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao(); ;
                string query = "DELETE FROM Admin WHERE id_Admin = @idAdmin";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idAdmin", idAdmin);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Conta exlcuída com sucesso.");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao deletar conta.\n" + e.Message);
                return false;
            }
        }
    }
}
