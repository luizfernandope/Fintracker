using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FinTracker.BD
{
    public class PagamentoRepository
    {
        private string _connectionString;

        public PagamentoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddPagamento(DateTime data, string metodo, string tipo, int parcelas, decimal valor)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Pagamento (Data, Método, Tipo, Parcelas, Valor) VALUES (@Data, @Metodo, @Tipo, @Parcelas, @Valor)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Data", data);
                cmd.Parameters.AddWithValue("@Metodo", metodo);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Parcelas", parcelas);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetPagamentos()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Pagamento", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdatePagamento(int idPagamento, DateTime data, string metodo, string tipo, int parcelas, decimal valor)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Pagamento SET Data = @Data, Método = @Metodo, Tipo = @Tipo, Parcelas = @Parcelas, Valor = @Valor WHERE id_Pagamento = @idPagamento";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idPagamento", idPagamento);
                cmd.Parameters.AddWithValue("@Data", data);
                cmd.Parameters.AddWithValue("@Metodo", metodo);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@Parcelas", parcelas);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePagamento(int idPagamento)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Pagamento WHERE id_Pagamento = @idPagamento";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idPagamento", idPagamento);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
