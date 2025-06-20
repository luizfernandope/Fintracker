using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinTracker.BD
{
    public class PagamentoRepository
    {
        public PagamentoRepository()
        {
        }

        public async void AddPagamentoAsync(Despesa d)
        {
            MySqlConnection conn = await MetodosDB.conexao();
                string query = "INSERT INTO Pagamento (Data, Metodo, Tipo, Parcelas, Valor, descricao) VALUES (@Data, @Metodo, @Tipo, @Parcelas, @Valor, @descricao)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Data", d.Data.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.Parameters.AddWithValue("@Metodo", d.Metodo);
                cmd.Parameters.AddWithValue("@Tipo", d.Tipo);
                cmd.Parameters.AddWithValue("@Parcelas", d.Parcelas);
                cmd.Parameters.AddWithValue("@Valor", d.Valor);
                cmd.Parameters.AddWithValue("@descricao", d.Descricao);
                cmd.ExecuteNonQuery();
            
        }

        public async Task<DataTable >GetPagamentos()
        {
            try
            {
                MySqlConnection conn = await MetodosDB.conexao();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM pagamento", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                await da.FillAsync(dt);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<DataTable> pesquisarPagamentos(string strBusca)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "SELECT * FROM Pagamento " +
                $"WHERE id_Pagamento = '{strBusca}' OR Data LIKE '%{strBusca}%' OR Metodo LIKE '%{strBusca}%' OR Tipo LIKE '%{strBusca}%' " +
                $"OR Parcelas LIKE '%{strBusca}%' OR Valor LIKE '%{strBusca}%' OR descricao LIKE '%{strBusca}%'";
            try
            {

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                await da.FillAsync(dt);
                return dt;
            }
            catch (Exception erro)
            {
                MessageBox.Show("Erro: " + erro.Message);
            }
            return null;
        }
        public async void UpdatePagamento(int idPagamento, DateTime data, string metodo, string tipo, int parcelas, decimal valor)
        {
            MySqlConnection conn = await MetodosDB.conexao();
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

        public async void DeletePagamento(int idPagamento)
        {
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "DELETE FROM Pagamento WHERE id_Pagamento = @idPagamento";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@idPagamento", idPagamento);
            cmd.ExecuteNonQuery();
            
        }
    }
}
