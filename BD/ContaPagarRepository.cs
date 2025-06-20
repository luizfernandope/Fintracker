using FinTracker.Interfaces;
using FinTracker.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace FinTracker.BD
{
    public class ContaPagarRepository
    {
        private string _connectionString;

        public ContaPagarRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ContaPagarRepository()
        {
        }

        public void AddContaPagar(decimal valor, int idFornecedor, string metodoPagamento, DateTime dataTransacao, DateTime? previsaoTermino)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Conta_a_Pagar (Valor, id_Fornecedor, Método_de_Pagamento, Data_de_Transação, Previsão_de_Término) VALUES (@Valor, @idFornecedor, @MetodoPagamento, @DataTransacao, @PrevisaoTermino)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                cmd.Parameters.AddWithValue("@MetodoPagamento", metodoPagamento);
                cmd.Parameters.AddWithValue("@DataTransacao", dataTransacao);
                cmd.Parameters.AddWithValue("@PrevisaoTermino", (object)previsaoTermino ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetContasPagar()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Conta_a_Pagar", conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public async Task<List<Conta>> pegarContas_a_pagar()
        {
            List<Conta> contas = new List<Conta>();
            MySqlConnection conn = await MetodosDB.conexao();
            string query = "SELECT * FROM conta_a_pagar";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = (MySqlDataReader) await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Conta conta = new Conta
                        {
                            id_Conta_a_Pagar = reader.GetInt32("id_Conta_a_Pagar"),
                            id_Fornecedor = reader.GetInt32("id_Fornecedor"),
                            valor = reader.GetDouble("Valor"),
                            metodoPagamento = reader.GetString("Metodo_de_Pagamento"),
                            dataTransacao = reader.GetDateTime("Data_de_Transacao"),
                            previsaoTermino = reader.GetDateTime("Previsao_de_Termino") ,
                            descricao = reader.GetString("Descricao")
                        };
                        conta.nomeFornecedor = await new FornecedorRepository().pegarNomeByid(conta.id_Fornecedor);
                        contas.Add(conta);
                    }
                }
            return contas;
        }

        public void UpdateContaPagar(int idContaPagar, decimal valor, int idFornecedor, string metodoPagamento, DateTime dataTransacao, DateTime? previsaoTermino)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "UPDATE Conta_a_Pagar SET Valor = @Valor, id_Fornecedor = @idFornecedor, Método_de_Pagamento = @MetodoPagamento, Data_de_Transação = @DataTransacao, Previsão_de_Término = @PrevisaoTermino WHERE id_Conta_a_Pagar = @idContaPagar";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idContaPagar", idContaPagar);
                cmd.Parameters.AddWithValue("@Valor", valor);
                cmd.Parameters.AddWithValue("@idFornecedor", idFornecedor);
                cmd.Parameters.AddWithValue("@MetodoPagamento", metodoPagamento);
                cmd.Parameters.AddWithValue("@DataTransacao", dataTransacao);
                cmd.Parameters.AddWithValue("@PrevisaoTermino", (object)previsaoTermino ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteContaPagar(int idContaPagar)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Conta_a_Pagar WHERE id_Conta_a_Pagar = @idContaPagar";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idContaPagar", idContaPagar);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
