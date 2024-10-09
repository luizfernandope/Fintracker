using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace FinTracker.BD
{
    public class ProdutoRepository
    {
        private string _connectionString;

        public ProdutoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddProduto(string nome, string categoria, string descricao, int quantidade, decimal valorUnitario, decimal valorTotal)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Produto (Nome, Categoria, Descrição, Quantidade, Valor_Unitário, Valor_Total) VALUES (@Nome, @Categoria, @Descrição, @Quantidade, @Valor_Unitário, @Valor_Total)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Categoria", categoria);
                cmd.Parameters.AddWithValue("@Descrição", descricao);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                cmd.Parameters.AddWithValue("@Valor_Unitário", valorUnitario);
                cmd.Parameters.AddWithValue("@Valor_Total", valorTotal);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetProdutos()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Produto", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public void UpdateProduto(int idProduto, string nome, string categoria, string descricao, int quantidade, decimal valorUnitario, decimal valorTotal)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Produto SET Nome = @Nome, Categoria = @Categoria, Descrição = @Descrição, Quantidade = @Quantidade, Valor_Unitário = @Valor_Unitário, Valor_Total = @Valor_Total WHERE id_Produto = @idProduto";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                cmd.Parameters.AddWithValue("@Nome", nome);
                cmd.Parameters.AddWithValue("@Categoria", categoria);
                cmd.Parameters.AddWithValue("@Descrição", descricao);
                cmd.Parameters.AddWithValue("@Quantidade", quantidade);
                cmd.Parameters.AddWithValue("@Valor_Unitário", valorUnitario);
                cmd.Parameters.AddWithValue("@Valor_Total", valorTotal);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduto(int idProduto)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Produto WHERE id_Produto = @idProduto";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idProduto", idProduto);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
