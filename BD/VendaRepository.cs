using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FinTracker.BD
{
    public class VendaRepository
    {
        private string _connectionString;

        public VendaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddVenda(int idProduto, int idPagamento, int idCliente, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Venda (id_Produto, id_Pagamento, id_Cliente, Status) VALUES (@idProduto, @idPagamento, @idCliente, @Status)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                cmd.Parameters.AddWithValue("@idPagamento", idPagamento);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetVendas()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Venda", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateVenda(int idVenda, int idProduto, int idPagamento, int idCliente, string status)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Venda SET id_Produto = @idProduto, id_Pagamento = @idPagamento, id_Cliente = @idCliente, Status = @Status WHERE id_Venda = @idVenda";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idVenda", idVenda);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                cmd.Parameters.AddWithValue("@idPagamento", idPagamento);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteVenda(int idVenda)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Venda WHERE id_Venda = @idVenda";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idVenda", idVenda);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
