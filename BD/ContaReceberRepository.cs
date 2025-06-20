using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FinTracker.BD
{
    public class ContaReceberRepository
    {
        private string _connectionString;

        public ContaReceberRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ContaReceberRepository()
        {
        }

        public void AddContaReceber(decimal valor, int idCliente, string metodoPagamento, DateTime dataTransacao, DateTime? previsaoTermino)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Conta_a_Receber (Valor, id_Cliente, Método_de_Pagamento, Data_de_Transação, Previsão_de_Término) VALUES (@Valor, @idCliente, @MetodoPagamento, @DataTransacao, @PrevisaoTermino)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@MetodoPagamento", metodoPagamento);
                cmd.Parameters.AddWithValue("@DataTransacao", dataTransacao);
                cmd.Parameters.AddWithValue("@PrevisaoTermino", (object)previsaoTermino ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetContasReceber()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Conta_a_Receber", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public async Task<List<Conta>> pegarContas_a_receber()
        {
            List<Conta> contas = new List<Conta>();
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "SELECT * FROM conta_a_receber";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using (MySqlDataReader reader = (MySqlDataReader)await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Conta conta = new Conta
                    {
                        id_Conta_a_Pagar = reader.GetInt32("id_Conta_a_Receber"),
                        id_Fornecedor = reader.GetInt32("id_Cliente"),
                        valor = reader.GetDouble("Valor"),
                        metodoPagamento = reader.GetString("Metodo_de_Pagamento"),
                        dataTransacao = reader.GetDateTime("Data_de_Transacao"),
                        previsaoTermino = reader.GetDateTime("Previsao_de_Termino"),
                        descricao = reader.GetString("Descricao")

                    };
                    conta.nomeCliente = await new ClienteRepository().pegarNomeByid(conta.id_Fornecedor);
                    contas.Add(conta);
                }
            }
            return contas;
        }

        public void UpdateContaReceber(int idContaReceber, decimal valor, int idCliente, string metodoPagamento, DateTime dataTransacao, DateTime? previsaoTermino)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Conta_a_Receber SET Valor = @Valor, id_Cliente = @idCliente, Método_de_Pagamento = @MetodoPagamento, Data_de_Transação = @DataTransacao, Previsão_de_Término = @PrevisaoTermino WHERE id_Conta_a_Receber = @idContaReceber";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idContaReceber", idContaReceber);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@MetodoPagamento", metodoPagamento);
                cmd.Parameters.AddWithValue("@DataTransacao", dataTransacao);
                cmd.Parameters.AddWithValue("@PrevisaoTermino", (object)previsaoTermino ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteContaReceber(int idContaReceber)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Conta_a_Receber WHERE id_Conta_a_Receber = @idContaReceber";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idContaReceber", idContaReceber);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
